import numpy as np
import random
from itertools import accumulate
import pandas as pd

class InstanceGenerator:
    def __init__(self, dim, err=0, dens=0):
        self.dimension = dim
        self.error_number = err
        self.density = dens                                                  #denisty is max number of possible 1s in one row
        self.matrix = np.array(np.zeros((dim, dim)))                         #other way l = np.array(np.ones(dim*dim))   l.reshape(dim, dim)

    def fill_matrix(self):
        if self.density == 0:
            self.density = self.dimension - 1                               #by default density is hardest case which means max number of 1s per row is possible (dim - 1)

        for ind, row in enumerate(self.matrix):
            possible_ones = random.randint(2, self.density)                  #we dont create metrices with rows containin only 1es nor having 0 nor only one 1es in row
            max_insertion_index = self.dimension - possible_ones
            prob = random.randint(0, 1)
            if ind < max_insertion_index:
                if prob:
                    row[ind:(ind+possible_ones)] = 1
                else:
                    row[0:possible_ones] = 1
            else:
                if prob:
                    row[max_insertion_index:max_insertion_index+possible_ones] = 1
                else:
                    row[0:possible_ones] = 1
        print(f'Instance without errrors:\n {self.matrix}')
        if self.error_number != 0:
            self.add_errors()
        self.check_columns()

    def check_columns(self):                                                         #now check if any column is only 0s
        for column in self.matrix.T:
            if not any(column):                                                      #if any column is only 0  // np.where(column==1) gives empty list when column doesnt have 1es
                print(f'ERROR KOLUMN \n {self.matrix}')
                self.matrix = np.array(np.zeros((self.dimension, self.dimension)))
                self.fill_matrix()

    def add_errors(self):
        added_errors_neg, added_errors_pos = 0, 0
        negative_errors, positive_errors = self.error_number // 2, self.error_number - self.error_number // 2
        #for row in range(self.dimension):
        #positive errors
        ll = random.sample(range(0, self.dimension), k=self.dimension)
        for row in ll:
            if added_errors_pos == positive_errors:
                break
            indices = np.where(self.matrix[row] == 0)
            if len(indices[0]) == 1:                                                 #for positive error i cant put in 1 when there are dim-1 1s in row already
                continue
            else:
                for index in indices[0]:
                    if index == 0:
                        if self.matrix[row][index+1] == 0:
                            self.matrix[row][index] = 1
                            print(f'Positive error added in {row}')
                            added_errors_pos += 1
                            break
                    elif index == self.dimension - 1:
                        if self.matrix[row][index-1] == 0:
                            self.matrix[row][index] = 1
                            print(f'Positive error added in {row}')
                            added_errors_pos += 1
                            break
                    else:
                        if self.matrix[row][index-1] == 0 and self.matrix[row][index+1] == 0:
                            self.matrix[row][index] = 1
                            print(f'Positive error added in {row}')
                            added_errors_pos += 1
                            break
        #negative errors
        for row in random.sample([i for i in range(self.dimension)], k=self.dimension):
            if added_errors_neg == negative_errors:
                break
            indices = np.where(self.matrix[row] == 1)
            if len(indices[0]) <= 2:
                continue
            else:
                for index in indices[0]:
                    if index == self.dimension - 1 or index == 0:
                        continue
                    elif self.matrix[row][index + 1] == 1 and self.matrix[row][index - 1] == 1:
                        self.matrix[row][index] = 0
                        print(f'Negative error added in {row}')
                        added_errors_neg += 1
                        break
        if added_errors_pos + added_errors_neg < self.error_number:
            raise Exception(f'Not enough errors added in instance of dimension {self.dimension} and error number of {self.error_number} and density of {self.density}')

    def shuffle_columns(self):
        np.random.shuffle(self.matrix.T)         #this transposes matrix then shuffle and then trasposes it back - > this way we have shuffled columns

    def print_matrix(self):
        print(self.matrix)

    def save_matrix(self, number, err_number):
        np.savetxt(f'./solutions/sol{number}_{err_number}err.csv', self.matrix, fmt='%.0f', delimiter=',', header=', '.join([f'{i}' for i in range(self.dimension)]))

    def save_shuffled_matrix(self, number, err_number):
        np.savetxt(f'./instances/inst{number}_{err_number}err.csv', self.matrix, fmt='%.0f', delimiter=',')

def count_size(val):
    result = 0
    for i in range(val):
        result += (1 + i)
    return result

def find_posibilities(interruptions):
    size = interruptions
    LL = [[str(i + 1) for i in range(size)] for _ in range(count_size(size))]
    flag = 0
    block_counter = 1
    inblock_line_counter = 0
    for i, li in enumerate(LL):
        if flag == 0:
            if i + 1 == size:
                flag = 1
            if i == 0:
                continue
            for j in range(i):
                li[-(j + 1)] += '1'
        else:
            block_blocker = size - block_counter
            while inblock_line_counter < block_blocker:
                for j in range(block_counter):
                    li[j] += '0'
                if inblock_line_counter == 0:
                    inblock_line_counter += 1
                    break
                if inblock_line_counter > 0:
                    for j in range(inblock_line_counter):
                        li[-(j + 1)] += '1'
                    inblock_line_counter += 1
                    break
            if inblock_line_counter == block_blocker:
                block_counter += 1
                inblock_line_counter = 0
    return LL

def count_posibilities(ones_indexes, pos):
    gaps_lengths = [ones_indexes[i + 1] - ones_indexes[i] - 1 for i in range(len(ones_indexes) - 1) if not ones_indexes[i + 1] == ones_indexes[i] + 1]
    gaps_indexes = [(ones_indexes[i] + 1, ones_indexes[i + 1] - 1) for i in range(len(ones_indexes) - 1) if not ones_indexes[i + 1] == ones_indexes[i] + 1]
    costs = {}
    for value in set(
            [pos[i][j] for i in range(len(pos)) for j in range(len(pos[i]))]):  # splaszcza liste list i robi seta
        if len(value) == 1:
            costs[value] = gaps_lengths[int(value) - 1]
        else:
            gap_num = value[0]
            aft_or_bef = value[1]
            if int(gap_num) == len(gaps_lengths) and int(aft_or_bef) == 1:  # jesli ostatni gap to tyko biore po uwage to co za nim
                costs[value] = max(ones_indexes) - gaps_indexes[int(gap_num) - 1][1]
            elif int(gap_num) == 1 and int(aft_or_bef) == 0:  # jesli pierwszy gap to tylko to co przed nim
                costs[value] = gaps_indexes[0][0] - min(ones_indexes)
            elif int(aft_or_bef) == 1:
                costs[value] = gaps_indexes[int(gap_num)][0] - gaps_indexes[int(gap_num) - 1][1] - 1
            else:
                costs[value] = gaps_indexes[int(gap_num) - 1][0] - gaps_indexes[int(gap_num) - 2][1] - 1
    return costs

class GeneticAlgorithm:
    def __init__(self, instance, pop_size, iters, cros, mut):
        self.matrix = instance
        self.dimension = self.matrix.shape[0]
        self.columns = dict(enumerate(self.matrix.T, 1))         #ll = [2, 5, 4, 1, 3]  new = np.array([columns[i] for i in ll]).T
        self.population = {}
        self.population_size = pop_size
        self.iteraton_number = iters
        self.crossing_probability = cros
        self.mutation_prob = mut
        self.solution = [None, -1, -1]                                 #size of 2 list - individual matrix and its fitness

    def exec(self):
        ON = int(input('Check the instance and soolution. Should I run?'))
        if ON == True:
            print('**************Genetic algorithm new run****************\n')
            self.create_starting_population()
            print('Innitial population created')
            counter = 0
            while counter < self.iteraton_number:
                print(f'\n{counter} GENERATION')
                self.compute_fitness(counter)
                print('Fitness computed')
                print(f'Solution in iteration: {self.solution[2]} with goal function {self.solution[1]}: {self.solution[0]}')
                if self.stop_function():
                     pass
                self.reproduction()
                print('Paired individuals have reproducted, otheres whill be copied to the next generation')
                self.mutate()
                print('Mutation was implemented')
                counter += 1
        else:
            return

    def create_starting_population(self):  #100 wektorów z indeksami kolumn
        for i in range(self.population_size):
            individual = random.sample(range(1, self.dimension + 1), self.dimension)
            self.population[i] = [individual.copy(), None]
        print(self.population)

    def compute_fitness(self, iteration):
        fitttt = []
        for i in range(self.population_size):
            individual_matrix = np.array([self.columns[j] for j in self.population[i][0]]).T
            mutations = self.population[i][2:]
            for mutation in mutations:
                individual_matrix[mutation[0]][mutation[1]] = 1 if individual_matrix[mutation[0]][mutation[1]] == 0 else 0
            individual_fitness = 0
            for row in individual_matrix:
                ones_indexes = np.where(row == 1)[0]
                number_of_interruptions = list(map(lambda a, b: b == a+1, ones_indexes, ones_indexes[1:])).count(False)
                if number_of_interruptions > 0:
                    if len(ones_indexes) == self.dimension - 2:
                        if number_of_interruptions == 1:
                            if row[0] == 0 or row[-1] == 0:
                                individual_fitness += 1
                            else:
                                o_indexes = np.where(row == 0)[0]
                                a1, a2 = np.split(row, [o_indexes[1]])
                                row_fitness = len(a1)-1 if len(a1) < len(a2) else len(a2) - 1
                                individual_fitness += row_fitness
                        else:                                                                                           #2 przerwania max
                            o_indexes = np.where(row == 0)[0]
                            a1, a2 = np.split(row, [o_indexes[0] + 1, o_indexes[1]])[0], np.split(row, [o_indexes[0] + 1, o_indexes[1]])[2]
                            row_fitness = len(a1) - 1 if len(a1) < len(a2) else len(a2) - 1 + 1
                            individual_fitness += row_fitness
                    elif len(ones_indexes) == 2:
                        if ones_indexes[1] == ones_indexes[0] + 2:
                            individual_fitness += 1
                        else:
                            individual_fitness += 2
                    elif number_of_interruptions == 1:
                        #if jedno zero w przerwaniu
                        o_indexes = np.where(row == 0)[0]
                        if len(o_indexes) == 1:
                            if len(ones_indexes) == self.dimension - 1: #jesli same jedynki i 1 zero miedzy nimi
                                individual_fitness += 2
                            else:
                                individual_fitness += 1   #zamieniam 0 na jedynke
                        else:
                            #if >jedno zera w przerwaniu
                            gap_indexes = [(ones_indexes[i] + 1, ones_indexes[i + 1] - 1) for i in range(len(ones_indexes) - 1) if not ones_indexes[i + 1] == ones_indexes[i] + 1][0]
                            gap_len = gap_indexes[1] - gap_indexes[0] + 1
                            if gap_len % 2 == 0:
                                spliter_ind = int(gap_indexes[0] + (gap_len/2))
                            else:
                                spliter_ind = int(gap_indexes[0] + (gap_len//2))
                            a1, a2 = np.split(row, [spliter_ind])
                            if gap_len <= np.count_nonzero(a1==1) and gap_len <= np.count_nonzero(a2 == 1):
                                individual_fitness += gap_len
                                if np.count_nonzero(row == 1) + gap_len == self.dimension:
                                    individual_fitness += 1
                            else:
                                individual_fitness += min(np.count_nonzero(a2 == 1), np.count_nonzero(a1==1))
                    elif number_of_interruptions >= 2:
                        if number_of_interruptions == 2:
                            posibilities = [['1', '21'], ['1', '2'], ['10', '2'], ['10', '21'], ['11', '21']]   #sprawdzam określona kombinacje
                        else:
                            posibilities = find_posibilities(number_of_interruptions)
                        posibilities_costs = count_posibilities(ones_indexes, posibilities)
                        row_fitness = -1
                        for pos in posibilities:
                            total_cost = sum(posibilities_costs[i] for i in pos)
                            if total_cost < row_fitness or row_fitness == -1:
                                row_fitness = total_cost
                        individual_fitness += row_fitness
            self.population[i][1] = individual_fitness
            fitttt.append(individual_fitness)
            if individual_fitness < self.solution[1] or self.solution[1] == -1:
                self.solution = [self.population[i][0], individual_fitness, iteration]
        print(f'Najmniejszy fitnes tej generacji: {min(fitttt)} ')
        print(self.population)
        print(self.solution)

    def stop_function(self):
        #compute if we should stop the algorithm when results are not progressive
        pass

    def selection(self):
        ftns = [self.population[i][1] for i in range(self.population_size)]           #selection of n individuals that will be allowed to reproduct with roulette method
        dic = {}
        for i in range(max(ftns)):                                                           #d = dict(zip([l[i] for i in range(len(l)//2)], [l[i] for i in range(len(l)%2)]))  -> not working correctly
            dic[min(ftns) + i] = max(ftns) - i
        ftns = list(map(lambda x: dic[x], ftns))                                            #or just [dick[i] for i in ftns]
        probs = np.array(ftns) / sum(ftns)                                                                        #roulette_values = list(accumulate(ftns))  -> this would be useful if not the random choces method
        selection_size = int(self.population_size*self.crossing_probability)
        if selection_size % 2 != 0:
            selection_size += 1
        selected = np.random.choice(list(self.population.keys()), selection_size, p=list(probs), replace=False)               #for i in range(self.population_size * self.selection_percent):
        #print(f'Selected: {selected}')
        return list(selected)                                                                                                      #random.randint(1, max(roulette_values))

    def reproduction(self):
        #selected individuals are randomly paired, but not all of them. If the crossing probability is less than 1 for example 0.85 -> 15% rodzicow jest skopiowana
        #some individuals will be copied to next population (the best ones or random ones)
        #every crossing of two individuals results in two new individuals
        #method of crossing is to be chosen - one, two, or multiple punctor method or binary vector method
        selected = self.selection()
        print('Individuals ought to pair were selected')
        #copied = [i for i in list(self.population.keys()) if i not in selected]
        pairs = []
        while any(selected):
            ind_1 = selected.pop(random.randrange(0, len(selected)))
            ind_2 = selected.pop(random.randrange(0, len(selected)))
            pairs.append((ind_1, ind_2))
        chain_len = round(0.4 * self.dimension)
        for parents in pairs:
            chromosome_1 = self.population[parents[0]][0]
            chromosome_2 = self.population[parents[1]][0]
            chain_start_indx = random.randint(1, self.dimension - chain_len)
            chain_end_indx = chain_start_indx + chain_len
            initial_subs = {chromosome_1[i]: chromosome_2[i] for i in range(chain_start_indx, chain_end_indx)}
            subs = {}
            vals = list(initial_subs.values())
            for key in list(initial_subs.keys()):
                if key in vals:  # abc
                    x = [i for i in initial_subs if initial_subs[i] == key][0]
                    if x in subs.keys():
                        continue
                    subs[x] = initial_subs[key]
                    subs[initial_subs[key]] = x
                else:
                    if key in subs.keys():
                        continue
                    subs[key] = initial_subs[key]
                    subs[initial_subs[key]] = key
            chromosome_1 = [subs[i] if subs.get(i) else i for i in chromosome_1]
            chromosome_2 = [subs[i] if subs.get(i) else i for i in chromosome_2]
            self.population[parents[0]][0] = chromosome_1
            self.population[parents[1]][0] = chromosome_2

    def mutate(self):
        #very few individuals are affected by muatation based on muattion probability
        num_to_mutate = round(self.population_size * self.mutation_prob)
        if num_to_mutate == 0:
            num_to_mutate = 1
        mutated = random.sample(list(self.population.keys()), num_to_mutate)
        for individual in mutated:
            matrix_to_mutate = np.array([self.columns[i] for i in self.population[individual][0]]).T
            possible_rows = random.sample(range(0, self.dimension), self.dimension)
            for row in possible_rows:
                col = random.randint(0, self.dimension-1)
                if matrix_to_mutate[row][col] == 0 and not list(matrix_to_mutate[row] == 1).count(True) == self.dimension - 1:
                    self.population[individual].append((row, col))
                    break
                elif matrix_to_mutate[row][col] == 1 and not list(matrix_to_mutate[row] == 1).count(True) == 2:
                    self.population[individual].append((row, col))
                    break


def main():
    i1 = InstanceGenerator(15, 4, 7)                     #dimension and error count and degree of fulfillment of ones (density)
    i1.fill_matrix()
    print('Rozwiązanie z błędami: ')
    i1.print_matrix()
    i1.save_matrix(1, 2)                             #instance number and error count
    i1.shuffle_columns()
    print('Instancja na wejście: ')
    i1.print_matrix()
    i1.save_shuffled_matrix(1, 2)

    g1 = GeneticAlgorithm(i1.matrix, 100, 20, 0.5, 0.04)
    g1.exec()
    del i1
    del g1

if __name__ == '__main__':
    main()

