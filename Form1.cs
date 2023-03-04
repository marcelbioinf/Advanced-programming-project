using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace projekt
{ 
    public partial class Form1 : Form
    {
        volatile bool Active = true;
        volatile bool Stop = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // ----- KOD 2/3 ZAKŁADKI - METAHEURYSTYKA --------

        static GeneticAlgorithm ga;
        int[,] solution_matrix = null;
        int[] solution = null;
        static DataTable SolutionTable;
        static BindingSource SolutionBind;

        class GeneticAlgorithm
        {
            private int[,] matrix;
            private int dimension_m;   //rows
            private int dimension_n;   //cols
            private List<int[]> population;
            private List<int> fitness;
            private List<int[]> mutations;  //nr osobnika, col, col
            private IDictionary<int, int[]> columns;
            private int population_size;
            private double crossing_probability;
            private double mutation_probability;
            private int[] solution;
            private int[] solution_sequence;  

            public GeneticAlgorithm(int[,] instance, int pop_size, double cros, double mut)
            {
                population = new List<int[]>();
                mutations = new List<int[]>();
                fitness = new List<int>();
                columns = new Dictionary<int, int[]>();
                solution = new int[2] { -1, -1 };
                solution_sequence = new int[instance.GetLength(1)];
                matrix = instance;
                dimension_m = instance.GetLength(0);
                dimension_n = instance.GetLength(1);
                population_size = pop_size;
                crossing_probability = cros;
                mutation_probability = mut;
                for (int i = 0; i < dimension_n; i++)
                {
                    columns.Add(i, Extensions.GetColumn(matrix, i));
                }
            }
 
            public void Create_starting_population()
            {
                var individual = Enumerable.Range(0, dimension_n).ToArray();
                for (int i = 0; i < population_size; i++)
                {
                    individual.Shuffle();
                    int[] ind_second = individual.ToArray();
                    population.Add(ind_second);
                }
            }

            public void Compute_fitness()
            {
                fitness.Clear();
                for (int i = 0; i < population_size; i++)
                {
                    int[,] individual_matrix = new int[dimension_m, dimension_n];
                    int col_idx = 0;
                    foreach (var num in population[i])   //odtwarzam moja macierz na podstawie kolejnosci kolumn danego osobnika
                    {
                        var col = columns[num];
                        for (int k = 0; k < dimension_m; k++)
                        {
                            individual_matrix[k, col_idx] = col[k];
                        }
                        col_idx += 1;
                    }
                    int individual_fitness = 0;
                    for (int j = 0; j < dimension_m; j++)   //iteruje po wierszach danej macierzy
                    {
                        var row = Extensions.GetRow(individual_matrix, j);
                        var ones_indexes = Enumerable.Range(0, row.Length).Where(x => row[x] == 1).ToArray();
                        var zeros_indexes = Enumerable.Range(0, row.Length).Where(x => row[x] == 0).ToArray();
                        int number_of_interruptions = 0;
                        for (int oi = 1; oi < ones_indexes.Length; oi++)
                        {
                            if (ones_indexes[oi] != ones_indexes[oi - 1] + 1)
                            {
                                number_of_interruptions += 1;
                            }
                        }
                        if (number_of_interruptions > 0)
                        {
                            if (ones_indexes.Length == dimension_n - 2)   //jesli dim - 2 jedynek
                            {
                                if (number_of_interruptions == 1)  //jeśli jedno przerwanie 
                                {
                                    if (row[0] == 0 || row.Last() == 0)
                                    {
                                        individual_fitness += 1;
                                    }
                                    else
                                    {
                                        int[] firstHalf = row.Take(zeros_indexes[1]).ToArray();
                                        int[] secondHalf = row.Skip(zeros_indexes[1]).ToArray();
                                        if (firstHalf.Length < secondHalf.Length)
                                        {
                                            individual_fitness += firstHalf.Length - 1;
                                        }
                                        else
                                        {
                                            individual_fitness += secondHalf.Length - 1;
                                        }
                                    }
                                }
                                else
                                {
                                    int[] firstHalf = row.Take(zeros_indexes[0] + 1).ToArray();
                                    int[] secondHalf = row.Skip(zeros_indexes[1]).ToArray();
                                    if (firstHalf.Length < secondHalf.Length)
                                    {
                                        individual_fitness += firstHalf.Length - 1 + 1;
                                    }
                                    else
                                    {
                                        individual_fitness += secondHalf.Length - 1 + 1;
                                    }
                                }
                            }
                            else if (ones_indexes.Length == 2)
                            {
                                if (ones_indexes[1] == ones_indexes[0] + 2)
                                {
                                    individual_fitness += 1;
                                }
                                else
                                {
                                    individual_fitness += 2;
                                }
                            }
                            else if (number_of_interruptions == 1)  //1 przerwanie
                            {
                                if (zeros_indexes.Length == 1)  // 1 zero w przerwaniu
                                {
                                    if(zeros_indexes[0] == 1 || zeros_indexes[0] == dimension_n - 2)
                                    {
                                        individual_fitness += 1;
                                    }
                                    else
                                        individual_fitness += 2;
                                }
                                else  //wiecej 0 w przerwaniu
                                {
                                    var gap_indexes = new List<int>();
                                    for (int g = 0; g < ones_indexes.Length - 1; g++)
                                    {
                                        if (ones_indexes[g + 1] != ones_indexes[g] + 1)
                                        {
                                            gap_indexes.Add(ones_indexes[g] + 1);
                                            gap_indexes.Add(ones_indexes[g + 1] - 1);
                                        }
                                    }
                                    int gap_len = gap_indexes[1] - gap_indexes[0] + 1;
                                    int splitter_ind = gap_indexes[0] + (gap_len / 2);
                                    if (gap_len % 2 == 0)
                                    {
                                        splitter_ind -= 1;
                                    }
                                    int[] firstHalf = row.Take(splitter_ind).ToArray();
                                    int[] secondHalf = row.Skip(splitter_ind).ToArray();
                                    if (gap_len <= firstHalf.Count(x => x == 1) && gap_len <= secondHalf.Count(x => x == 1))
                                    {
                                        individual_fitness += gap_len;
                                        if (row.Count(x => x == 1) + gap_len == dimension_n)
                                        {
                                            individual_fitness += 1;
                                        }
                                    }
                                    else
                                    {
                                        individual_fitness += Math.Min(firstHalf.Count(x => x == 1), secondHalf.Count(x => x == 1));
                                    }

                                }
                            }
                            else if (number_of_interruptions >= 2)
                            {
                                List<string[]> posibilities = new List<string[]>
                                {
                                    new string[] {"1", "21"},
                                    new string[] {"1", "2"},
                                    new string[] {"10", "2"},
                                    new string[] {"10", "21"},
                                    new string[] {"11", "21"},
                                };
                                if (number_of_interruptions > 2)
                                {
                                    posibilities.Clear();
                                    posibilities = Extensions.Find_Possibilities(number_of_interruptions);
                                }
                                var posibilities_costs = Extensions.Count_posibilities(ones_indexes, posibilities);
                                int row_fitness = -1;
                                foreach (var pos in posibilities)
                                {
                                    int total_cost = 0;
                                    foreach (var nn in pos)
                                    {
                                        total_cost += posibilities_costs[nn];
                                    }
                                    if (total_cost < row_fitness || row_fitness == -1)
                                    {
                                        row_fitness = total_cost;
                                    }
                                }
                                individual_fitness += row_fitness;

                            }
                        }
                    }
                    fitness.Add(individual_fitness);
                    if (individual_fitness < solution[1] || solution[1] == -1)
                    {
                        solution[0] = i;
                        solution[1] = individual_fitness;
                        solution_sequence = population[i].ToArray();
                    }
                }
            }

            public string print_fitness()
            {
                string ms = "";
                ms += "Lowest fitness of this generation: ";
                ms += fitness.Min().ToString();
                return ms;
            }

            public string print_solution()
            {
                string ms = " Individual and its fitness: ";
                foreach (var a in solution)
                {
                    ms += a.ToString();
                    ms += " ";
                }
                return ms;
            }

            public string print_solution_sequence()
            {
                string ms = "Solution columns:  ";
                foreach (var a in solution_sequence)
                {
                    ms += a.ToString();
                    ms += " ";
                }
                return ms;
            }

            public int give_solution_fitness()
            {
                return solution[1];
            }

            public List<int> Selection()
            {
                var dic = new Dictionary<int, int>();
                for(int i = 0; i <= fitness.Max(); i++)
                {
                    dic.Add(fitness.Min() + i, fitness.Max() - i);
                }
                List<int> fitness_computed = new List<int>();
                int value;
                foreach(var i in fitness)
                {
                    if (dic.TryGetValue(i, out value))
                    {
                        fitness_computed.Add(dic[i]);
                    }
                    else
                    {
                        MessageBox.Show("Max: " + fitness.Max().ToString());
                        MessageBox.Show("Min: " + fitness.Min().ToString());
                        MessageBox.Show("Nie ma w słowniku: "+i.ToString());
                    }
                }
                List<float> probs = new List<float>();
                foreach(float i in fitness_computed)
                {
                    probs.Add(i / fitness_computed.Sum());
                }
                int selection_size = (int)(population_size * crossing_probability);
                if(selection_size % 2 != 0)
                {
                    selection_size -= 1;
                }
                Random r = new Random();
                List<int> Selected = new List<int>();
                while (Selected.Count() < selection_size)
                {
                    //bez powtorzen
                    double diceRoll = r.NextDouble();
                    double cumulative = 0.0;
                    for (int i = 0; i < population_size; i++)
                    {
                        cumulative += probs[i];
                        if (diceRoll < cumulative && !(Selected.Contains(i)))
                        {
                            Selected.Add(i);
                            break;
                        }
                    }
                }
                return Selected;
            }

            public void Reproduction() 
            {
                var Selected = this.Selection();
                List<int[]> pairs = new List<int[]>();
                Random random = new Random();
                while(Selected.Count() > 0)
                {
                    int rn1 = random.Next(0, Selected.Count());
                    int rn2 = random.Next(0, Selected.Count());
                    while(rn1 == rn2)
                    {
                        rn2 = random.Next(0, Selected.Count());
                    }
                    pairs.Add(new int[2] { Selected[rn1], Selected[rn2] });
                    Selected.RemoveAll(t => t == Selected[rn1] || t == Selected[rn2]);
                }
                int chain_len = (int) (0.4 * dimension_n);
                foreach(var parents in pairs)
                {
                    var chromosome_1 = population[parents[0]];
                    var chromosome_2 = population[parents[1]];
                    var chain_start_indx = random.Next(1, (dimension_n - chain_len + 1));
                    var chain_end_indx = chain_start_indx + chain_len;
                    Dictionary<int, int> initial_subs = new Dictionary<int, int>();
                    for(int i = chain_start_indx; i < chain_end_indx; i++)
                    {
                        initial_subs.Add(chromosome_1[i], chromosome_2[i]);
                    }
                    Dictionary<int, int> subs = new Dictionary<int, int>();
                    var keys = initial_subs.Keys.ToList();
                    foreach (var vall in initial_subs.Values.ToList())
                    {
                        if (keys.Contains(vall))
                        {
                            int out_val;
                            int flag = 1;
                            int val_to_search = vall;
                            int counter = 0;
                            while (initial_subs.TryGetValue(val_to_search, out out_val))
                            {
                                if (counter > initial_subs.Count * 2)
                                {
                                    flag = 0;
                                    break;
                                }
                                val_to_search = out_val;
                                counter += 1;
                            }
                            if (flag == 1)
                            {
                                initial_subs[val_to_search] = initial_subs.FirstOrDefault(y => y.Value == vall).Key; ;
                            }
                        }
                        else
                        {
                            if (initial_subs.Keys.Contains(vall))
                            {
                                continue;
                            }
                            initial_subs[vall] = initial_subs.FirstOrDefault(y => y.Value == vall).Key;
                        }
                    }
                    var chromosome_1_new = new int[dimension_n];
                    var chromosome_2_new = new int[dimension_n];
                    int val = 0;
                    for(int i= 0; i < chromosome_1.Length; i++)
                    {
                        if (subs.TryGetValue(chromosome_1[i], out val))
                        {
                            chromosome_1_new[i] = val;
                        }
                        else
                        {
                            chromosome_1_new[i] = chromosome_1[i];
                        }
                    }
                    for (int i = 0; i < chromosome_2.Length; i++)
                    {
                        if (subs.TryGetValue(chromosome_2[i], out val))
                        {
                            chromosome_2_new[i] = val;
                        }
                        else
                        {
                            chromosome_2_new[i] = chromosome_2[i];
                        }
                    }
                    HashSet<int> set1 = new HashSet<int>(chromosome_1_new);
                    HashSet<int> set2 = new HashSet<int>(chromosome_2_new);
                    if (set1.Count != dimension_n || set2.Count != dimension_n){
                        throw new InvalidOperationException("NIEPRAWIDLOWE ROZWIAZANIE");
                    }
                    population[parents[0]] = chromosome_1_new;
                    population[parents[1]] = chromosome_2_new;
                }
            }

            public void Mutation()
            {
                int num_to_mutate = (int)(population_size * mutation_probability);
                if(num_to_mutate == 0)
                {
                    num_to_mutate = 1;
                }
                Random rnd = new Random();
                var pos = Enumerable.Range(0, population_size).ToList();
                var mutated = pos.OrderBy(x => rnd.Next()).Take(num_to_mutate);
                foreach(var individual in mutated)
                {
                    var chr_to_mutate = population[individual];
                    int idx1 = rnd.Next(0, dimension_n);
                    int idx2 = rnd.Next(0, dimension_n);
                    while (idx1 == idx2)
                    {
                       idx2 = rnd.Next(0, dimension_n);
                    }
                    (chr_to_mutate[idx1], chr_to_mutate[idx2]) = (chr_to_mutate[idx2], chr_to_mutate[idx1]);
                    population[individual] = chr_to_mutate;
                    mutations.Add(new int[] {individual, idx1, idx2 });
                }
            }

            public int[] return_solution_sequence()
            {
                return solution_sequence;
            }
        }

        private void Start_meta_btn_Click(object sender, EventArgs e)
        {
            Start_meta_btn.Enabled = false;
            if (!read_checkb.Checked)
            {
                MessageBox.Show("Check if your instance is ready in Generator Instance checkbox");
            }
            else
            {
                int pops = int.Parse(Populationsize_textbox.Text);
                int iternum = int.Parse(Iteration_textbox.Text);
                double cp = double.Parse(crossprob_textbox.Text);
                double mp = double.Parse(mutprob_textbox.Text);
                bool flag = true;

                if (string.IsNullOrEmpty(Populationsize_textbox.Text))
                {
                    MessageBox.Show("Insert population size");
                    flag = false;
                }
                else if (pops < 5 || pops > 10000)
                {
                    MessageBox.Show("Given population size is invalid. Minimum is 5 and maximum is 500");
                    flag = false;
                }
                if (string.IsNullOrEmpty(Iteration_textbox.Text))
                {
                    MessageBox.Show("Insert iteration number");
                    flag = false;
                }
                else if (iternum < 1 || iternum > 300)
                {
                    MessageBox.Show("Given iteration number is invalid");
                    flag = false;
                }
                if (string.IsNullOrEmpty(crossprob_textbox.Text))
                {
                    MessageBox.Show("Insert crossing probability");
                    flag = false;
                }
                else if (cp > 1 || cp < 0)
                {
                    MessageBox.Show("Given crossing probability is invalid. Must be value between 0 and 1");
                    flag = false;
                }
                if (string.IsNullOrEmpty(mutprob_textbox.Text))
                {
                    MessageBox.Show("Insert mutation probability");
                    flag = false;
                }
                else if (mp < 0 || mp > 1)
                {
                    MessageBox.Show("Given mutation probability is invalid. Must be value between 0 and 1");
                    flag = false;
                }

                if (flag == true)
                {
                    progressBar1.Maximum = iternum;
                    progressBar1.Minimum = 0;
                    progressBar1.Value = 0;
                    BackgroundWorker bw = new BackgroundWorker();
                    bw.WorkerReportsProgress = true;
                    bw.DoWork += new DoWorkEventHandler(
                        delegate(object o, DoWorkEventArgs args) 
                        {
                            BackgroundWorker b = o as BackgroundWorker;
                            Stopwatch stopwatch = new Stopwatch();
                            stopwatch.Start();
                            ga = new GeneticAlgorithm(final_matrix, int.Parse(Populationsize_textbox.Text), double.Parse(crossprob_textbox.Text), double.Parse(mutprob_textbox.Text));
                            b.ReportProgress(1, "*********Genetic algorithm new run*******");
                            ga.Create_starting_population();
                            b.ReportProgress(2, "Initial population created");
                            int solution_value = -1;
                            int iteration_number = 0;
                            for (int i = 0; i < iternum; i++)
                            {
                                if(Active)
                                {
                                    b.ReportProgress(3, string.Format("{0} Generation", i));
                                    ga.Compute_fitness();
                                    b.ReportProgress(4, ga.print_fitness());
                                    string sol = ga.print_solution();
                                    if (ga.give_solution_fitness() < solution_value || solution_value == -1)
                                    {
                                        iteration_number = i;
                                        solution_value = ga.give_solution_fitness();
                                    }
                                    b.ReportProgress(5, string.Format("Solution in iteration {0} {1}", iteration_number.ToString(), sol));
                                    b.ReportProgress(6, ga.print_solution_sequence());
                                    ga.Reproduction();
                                    ga.Mutation();
                                    b.ReportProgress(8);
                                }
                                else
                                {
                                    i--;
                                }
                                if (Stop)
                                {
                                    Stop = false;
                                    b.ReportProgress(7, "STOPPED");
                                    break;
                                }
                            }
                            solution = ga.return_solution_sequence();
                            stopwatch.Stop();
                            b.ReportProgress(11, string.Format("Time elapsed {0} ms", stopwatch.ElapsedMilliseconds.ToString()));
                            b.ReportProgress(9, solution_value.ToString());
                        }
                    );
                    bw.ProgressChanged += new ProgressChangedEventHandler(
                        delegate (object o, ProgressChangedEventArgs args) {
                            if(args.ProgressPercentage == 11) //wypisanie czasu
                            {
                                Main_log_txtbox.AppendText((args.UserState as String) + Environment.NewLine);
                                time_txtbox.Text = (args.UserState as String);
                            }
                            if (args.ProgressPercentage == 9)   //jesli koniec
                            {
                                solution_matrix = new int[final_matrix.GetLength(0), final_matrix.GetLength(1)];
                                int col_idx = 0;
                                foreach (var num in solution)   
                                {
                                    var col = final_columns[num];
                                    for (int k = 0; k < final_matrix.GetLength(0); k++)
                                    {
                                        solution_matrix[k, col_idx] = col[k];
                                    }
                                    col_idx += 1;
                                }
                                SolutionTable = new DataTable();
                                SolutionBind = new BindingSource();
                                SolutionBind.DataSource = SolutionTable;
                                for (int i = 0; i < solution_matrix.GetLength(1); i++)
                                {
                                    SolutionTable.Columns.Add("C" + (i + 1));
                                }
                                for (var i = 0; i < solution_matrix.GetLength(0); ++i)
                                {
                                    DataRow row = SolutionTable.NewRow();
                                    for (var j = 0; j < solution_matrix.GetLength(1); ++j)
                                    {
                                        row[j] = solution_matrix[i, j];
                                    }
                                    SolutionTable.Rows.Add(row);
                                }
                                dataGridView2.DataSource = SolutionBind;
                                sol_txt_box.Text = (args.UserState as String);
                            }
                            else if (args.ProgressPercentage == 8)     //inkrementacje progrssbara
                            {
                                progressBar1.Value += 1;
                            }
                            else if(args.ProgressPercentage == 1)   //dodawanie do log txt boxa
                                Main_log_txtbox.AppendText(Environment.NewLine + (args.UserState as String) + Environment.NewLine);
                            else
                                Main_log_txtbox.AppendText((args.UserState as String) + Environment.NewLine);
                        }
                    );
                    bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                        delegate (object o, RunWorkerCompletedEventArgs args) {
                            Main_log_txtbox.AppendText("FINISHED, soluton available in third bar" + Environment.NewLine);
                        }
                    );
                    bw.RunWorkerAsync();
                }
            }
            Start_meta_btn.Enabled = true;
        }  

        private void clear_log_button_Click(object sender, EventArgs e)
        {
            Main_log_txtbox.Clear();
        }

        private void Pause_button_Click(object sender, EventArgs e)
        {
            if (Pause_button.Text == "Pause")
            {
                Active = false;
                Pause_button.Text = "Resume";
                Pause_button.BackColor = Color.MediumSeaGreen;
                Start_meta_btn.Enabled = false;
            }
            else
            {
                Active = true;
                Pause_button.Text = "Pause";
                Pause_button.BackColor = Color.LightGray;
                Pause_button.ForeColor = Color.Red;
                Start_meta_btn.Enabled = true;
            }
        }

        private void Stop_button_Click(object sender, EventArgs e)
        {
            Stop = true;
        }


        // ----- KOD 1 ZAKŁADKi - GENERATOR INSTANCJI ------

        static DataTable DTable;
        static BindingSource SBind;
        static Instance instance;
        int[,] final_matrix = null;
        IDictionary<int, int[]> final_columns = null;

        class Instance
        {
            private int dimension_m;
            private int dimension_n;
            private int error_number;
            private int density;
            private int[,] matrix;

            public Instance(int dim_m, int dim_n, int err, int dens)
            {
                dimension_m = dim_m;
                dimension_n = dim_n;
                error_number = err;
                density = dens;
                matrix = new int[dimension_m, dimension_n];
            }

            public void FillMatrix()
            {
                Random rnd = new Random();
                for (int k = 0; k < matrix.GetLength(0); k++)                 //iterating through rows
                {
                    int possible_ones = rnd.Next(density-2, density+1);
                    int max_insertion_index = dimension_n - possible_ones;
                    int prob = rnd.Next(0, 2);
                    if (k < max_insertion_index)
                    {
                        if (prob == 0)
                        {
                            for (int l = k; l < (k + possible_ones); l++)
                            {
                                matrix[k, l] = 1;
                            }
                        }
                        else
                        {
                            for (int l = 0; l < possible_ones; l++)
                            {
                                matrix[k, l] = 1;
                            }
                        }
                    }
                    else
                    {
                        if (prob == 0)
                        {
                            for (int l = max_insertion_index; l < (max_insertion_index + possible_ones); l++)
                            {
                                matrix[k, l] = 1;
                            }
                        }
                        else
                        {
                            for (int l = 0; l < possible_ones; l++)
                            {
                                matrix[k, l] = 1;
                            }
                        }
                    }
                }
                if (error_number != 0)
                {
                    this.Add_errors();
                }
                this.Check_columns();
            }

            public void Add_errors()
            {
                int added_neg_err = 0;
                int added_pos_err = 0;
                int pos_err = error_number / 2;
                int neg_err = error_number - error_number / 2;

                var rows = Enumerable.Range(0, dimension_m).ToArray();
                rows.Shuffle();
                foreach (var r in rows)  //adding positive errors
                {
                    if (added_pos_err == pos_err)
                    {
                        break;
                    }
                    var row = Extensions.GetRow(matrix, r);
                    var indices = Enumerable.Range(0, row.Length).Where(i => row[i] == 0).ToArray();
                    if (indices.Length == 1)
                    {
                        continue;
                    }
                    else
                    {
                        foreach (var idx in indices)
                        {
                            if (idx == 0)
                            {
                                if (matrix[r, idx + 1] == 0)
                                {
                                    matrix[r, idx] = 1;
                                    added_pos_err += 1;
                                    break;
                                }
                            }
                            else if (idx == dimension_n - 1)
                            {
                                if (matrix[r, idx - 1] == 0)
                                {
                                    matrix[r, idx] = 1;
                                    added_pos_err += 1;
                                    break;
                                }
                            }
                            else
                            {
                                if (matrix[r, idx - 1] == 0 && matrix[r, idx + 1] == 0)
                                {
                                    matrix[r, idx] = 1;
                                    added_pos_err += 1;
                                    break;
                                }
                            }
                        }
                    }
                }
                rows.Shuffle();
                foreach (var r in rows)  //adding negative errors
                {
                    if (added_neg_err == neg_err)
                    {
                        break;
                    }
                    var row = Extensions.GetRow(matrix, r);
                    var indices = Enumerable.Range(0, row.Length).Where(i => row[i] == 1).ToArray();
                    if (indices.Length <= 2)
                    {
                        continue;
                    }
                    else
                    {
                        foreach (var idx in indices)
                        {
                            if (idx == 0 || idx == dimension_n - 1)
                            {
                                continue;
                            }
                            else if (matrix[r, idx + 1] == 1 && matrix[r, idx - 1] == 1)
                            {
                                matrix[r, idx] = 0;
                                added_neg_err += 1;
                                break;
                            }
                        }
                    }
                }
                
                if(added_neg_err + added_pos_err < error_number)
                {
                   throw new InvalidOperationException("Za mało błędów dodanych w instancji");
                }
            }

            public void Check_columns()
            {
                for(int c = 0; c < dimension_n; c++)
                {
                    var column = Extensions.GetColumn(matrix, c);
                    var zeros = column.All(o => o == 0);
                    if (zeros)
                    {
                        matrix = new int[dimension_m, dimension_n];
                        this.FillMatrix();
                    }
                }
            }

            public void ShuffleColumns()
            {
                var transposed_array = new int[dimension_n, dimension_m];
                for (int j = 0; j < dimension_m; j++)
                {
                    for (int r = 0; r < dimension_n; r++)
                        transposed_array[r, j] = matrix[j, r];
                }
                var new_row_order = Enumerable.Range(0, dimension_n).ToArray();
                new_row_order.Shuffle();
                for(int j = 0; j < dimension_n; j++)
                {
                    for (int r = 0; r < dimension_m; r++)
                        matrix[r, j] = transposed_array[new_row_order[j], r];
                }

            }

            public int ComputeSolutionFitness()
            {
                int individual_fitness = 0;
                for (int j = 0; j < dimension_m; j++)   //iteruje po wierszach danej macierzy
                {
                    var row = Extensions.GetRow(matrix, j);
                    var ones_indexes = Enumerable.Range(0, row.Length).Where(x => row[x] == 1).ToArray();
                    var zeros_indexes = Enumerable.Range(0, row.Length).Where(x => row[x] == 0).ToArray();
                    int number_of_interruptions = 0;
                    for (int oi = 1; oi < ones_indexes.Length; oi++)
                    {
                        if (ones_indexes[oi] != ones_indexes[oi - 1] + 1)
                        {
                            number_of_interruptions += 1;
                        }
                    }
                    if (number_of_interruptions > 0)
                    {
                        if (ones_indexes.Length == dimension_n - 2)   //jesli dim - 2 jedynek
                        {
                            if (number_of_interruptions == 1)  //jeśli jedno przerwanie 
                            {
                                if (row[0] == 0 || row.Last() == 0)
                                {
                                    individual_fitness += 1;
                                }
                                else
                                {
                                    int[] firstHalf = row.Take(zeros_indexes[1]).ToArray();
                                    int[] secondHalf = row.Skip(zeros_indexes[1]).ToArray();
                                    if (firstHalf.Length < secondHalf.Length)
                                    {
                                        individual_fitness += firstHalf.Length - 1;
                                    }
                                    else
                                    {
                                        individual_fitness += secondHalf.Length - 1;
                                    }
                                }
                            }
                            else
                            {
                                int[] firstHalf = row.Take(zeros_indexes[0] + 1).ToArray();
                                int[] secondHalf = row.Skip(zeros_indexes[1]).ToArray();
                                if (firstHalf.Length < secondHalf.Length)
                                {
                                    individual_fitness += firstHalf.Length - 1 + 1;
                                }
                                else
                                {
                                    individual_fitness += secondHalf.Length - 1 + 1;
                                }
                            }
                        }
                        else if (ones_indexes.Length == 2)
                        {
                            if (ones_indexes[1] == ones_indexes[0] + 2)
                            {
                                individual_fitness += 1;
                            }
                            else
                            {
                                individual_fitness += 2;
                            }
                        }
                        else if (number_of_interruptions == 1)  //1 przerwanie
                        {
                            if (zeros_indexes.Length == 1)  // 1 zero w przerwaniu
                            {
                                if (ones_indexes.Length == dimension_n - 1)
                                {
                                    if (zeros_indexes[0] == 1 || zeros_indexes[0] == dimension_n - 2)
                                    {
                                        individual_fitness += 1;
                                    }
                                    else
                                        individual_fitness += 2;
                                }
                                else
                                {
                                    individual_fitness += 1;
                                }
                            }
                            else  //wiecej 0 w przerwaniu
                            {
                                var gap_indexes = new List<int>();
                                for (int g = 0; g < ones_indexes.Length - 1; g++)
                                {
                                    if (ones_indexes[g + 1] != ones_indexes[g] + 1)
                                    {
                                        gap_indexes.Add(ones_indexes[g] + 1);
                                        gap_indexes.Add(ones_indexes[g + 1] - 1);
                                    }
                                }
                                int gap_len = gap_indexes[1] - gap_indexes[0] + 1;
                                int splitter_ind = gap_indexes[0] + (gap_len / 2);
                                if (gap_len % 2 == 0)
                                {
                                    splitter_ind -= 1;
                                }
                                int[] firstHalf = row.Take(splitter_ind).ToArray();
                                int[] secondHalf = row.Skip(splitter_ind).ToArray();
                                if (gap_len <= firstHalf.Count(x => x == 1) && gap_len <= secondHalf.Count(x => x == 1))
                                {
                                    individual_fitness += gap_len;
                                    if (row.Count(x => x == 1) + gap_len == dimension_n)
                                    {
                                        individual_fitness += 1;
                                    }
                                }
                                else
                                {
                                    individual_fitness += Math.Min(firstHalf.Count(x => x == 1), secondHalf.Count(x => x == 1));
                                }

                            }
                        }
                        else if (number_of_interruptions >= 2)
                        {
                            List<string[]> posibilities = new List<string[]>
                                {
                                    new string[] {"1", "21"},
                                    new string[] {"1", "2"},
                                    new string[] {"10", "2"},
                                    new string[] {"10", "21"},
                                    new string[] {"11", "21"},
                                };
                            if (number_of_interruptions > 2)
                            {
                                posibilities.Clear();
                                posibilities = Extensions.Find_Possibilities(number_of_interruptions);
                            }
                            var posibilities_costs = Extensions.Count_posibilities(ones_indexes, posibilities);
                            int row_fitness = -1;
                            foreach (var pos in posibilities)
                            {
                                int total_cost = 0;
                                foreach (var nn in pos)
                                {
                                    total_cost += posibilities_costs[nn];
                                }
                                if (total_cost < row_fitness || row_fitness == -1)
                                {
                                    row_fitness = total_cost;
                                }
                            }
                            individual_fitness += row_fitness;

                        }
                    }
                }
                return individual_fitness;
            }

            public void ConvertToDtable()
            {
                DTable = new DataTable();
                SBind = new BindingSource();
                SBind.DataSource = DTable;
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    DTable.Columns.Add("C" + (i + 1));
                }

                for (var i = 0; i < matrix.GetLength(0); ++i)
                {
                    DataRow row = DTable.NewRow();
                    for (var j = 0; j < matrix.GetLength(1); ++j)
                    {
                        row[j] = matrix[i, j];
                    }
                    DTable.Rows.Add(row);
                }
            }
        }

        private void create_handinstance_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(columns_textbox.Text))
            {
                MessageBox.Show("Insert number of columns");
            }
            else
            {
                DTable = new DataTable();
                SBind = new BindingSource();
                SBind.DataSource = DTable;
                dataGridView1.DataSource = SBind;

                DTable.Clear();
                for (int i = 1; i <= int.Parse(columns_textbox.Text); i++)
                {
                    DTable.Columns.Add("C" + i);
                }
                read_checkb.Checked = false;
            }
        }

        private void addcol_button_Click(object sender, EventArgs e)
        {
            int num_cols = DTable.Columns.Count + 1;
            DTable.Columns.Add("C" + num_cols);
            read_checkb.Checked = false;
        }

        private void readfile_button_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                dataGridView1.DataSource = null;
                System.IO.StreamReader file = new System.IO.StreamReader(openFileDialog1.FileName);

                string[] columnnames = file.ReadLine().Split(',');
                DTable = new DataTable();
                foreach (string c in columnnames)
                {
                    DTable.Columns.Add(c);
                }
                string newline;
                while ((newline = file.ReadLine()) != null)
                {
                    DataRow row = DTable.NewRow();
                    string[] values = newline.Split(',');
                    for (int i = 0; i < values.Length ; i++)
                    {
                        row[i] = values[i];
                    }
                    DTable.Rows.Add(row);
                }
                file.Close();
                SBind = new BindingSource();
                SBind.DataSource = DTable;
                dataGridView1.DataSource = SBind;
                read_checkb.Checked = false;
            }
        }

        private void savefile_button_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(file_name_textbox.Text))
            {
                MessageBox.Show("Insert file's name");
            }
            else
            {
                string filepath = "C:\\Users\\marce\\hakowanie\\programming_c#\\projekt\\instances\\" + file_name_textbox.Text + ".txt";
                int i = 0;
                StreamWriter sw = null;
                sw = new StreamWriter(filepath, false);

                for (i = 0; i < DTable.Columns.Count - 1; i++)
                {
                    sw.Write(DTable.Columns[i].ColumnName + ",");
                }
                sw.Write(DTable.Columns[i].ColumnName);
                sw.WriteLine();

                foreach (DataRow row in DTable.Rows)
                {
                    object[] array = row.ItemArray;
                    for (i = 0; i < array.Length - 1; i++)
                    {
                        sw.Write(array[i].ToString() + ",");
                    }
                    sw.Write(array[i].ToString());
                    sw.WriteLine();
                }
                sw.Close();
                MessageBox.Show("Saved successfully");
            }

        }

        private void generate_button_Click(object sender, EventArgs e)  //create
        {
            int dim_m = int.Parse(dimension_m_textbox.Text);
            int dim_n = int.Parse(dimension_n_textbox.Text);
            int errors = int.Parse(errors_textbox.Text);
            int diff = int.Parse(difficulty_textbox.Text);
            int flag = 1;

            if (string.IsNullOrEmpty(difficulty_textbox.Text))
            {
                MessageBox.Show("Insert instance's difficulty");
                flag = 0;
            }
            else if (diff >= dim_n || diff < 1)
            {
                MessageBox.Show("Given difficulty is invalid");
                flag = 0;
            }
            if (string.IsNullOrEmpty(dimension_m_textbox.Text))
            {
                MessageBox.Show("Insert instance's dimension");
                flag = 0;
            }
            else if (dim_m < 3 || dim_m > 100)
            {
                MessageBox.Show("Given dimension is invalid");
                flag = 0;
            }
            if (string.IsNullOrEmpty(dimension_n_textbox.Text))
            {
                MessageBox.Show("Insert instance's dimension");
                flag = 0;
            }
            else if (dim_n < 3 || dim_n > 100)
            {
                MessageBox.Show("Given dimension is invalid");
                flag = 0;
            }
            if (string.IsNullOrEmpty(errors_textbox.Text))
            {
                MessageBox.Show("Insert instance's errors number");
                flag = 0;
            }
            else if (errors < 0 /*|| errors > dim_n*/)
            {
                MessageBox.Show("Given errors number is invalid");
                flag = 0;
            }
            if (flag == 1)
            {
                instance = new Instance(int.Parse(dimension_m_textbox.Text), int.Parse(dimension_n_textbox.Text), int.Parse(errors_textbox.Text), int.Parse(difficulty_textbox.Text));
                instance.FillMatrix();
                instance.ConvertToDtable();
                dataGridView1.DataSource = SBind;
                read_checkb.Checked = false;
            }
        }

        private void hand_generate_button_Click(object sender, EventArgs e)  //generate button
        {
            int dimension_m = DTable.Rows.Count;
            int dimension_n = DTable.Columns.Count;
            if (dimension_m > 2 && dimension_n > 2)
            {
                //create matrix ready for metaheuristic algorithm based on DTable 
                final_matrix = new int[dimension_m, dimension_n];
                for (int i = 0; i < dimension_m; i++)
                {
                    for (int j = 0; j < dimension_n; j++)
                    {
                        final_matrix[i, j] = int.Parse(DTable.Rows[i][j].ToString());
                    }
                }
                MessageBox.Show("Instancja wygenerowana");
                final_columns = new Dictionary<int, int[]>();
                for (int i = 0; i < dimension_n; i++)
                {
                    final_columns.Add(i, Extensions.GetColumn(final_matrix, i));
                }
            }
            else
            {
                MessageBox.Show("Stwórz odpowiednią instancję (o rozmiarze conajmniej 3)");
            }
        }   

        private void Shuffle_btn_Click(object sender, EventArgs e)
        {
            instance.ShuffleColumns();
            instance.ConvertToDtable();
            dataGridView1.DataSource = SBind;
        }

        private void cnt_fitness_btn_Click(object sender, EventArgs e)
        {
            if(instance != null)
            {
                var s = instance.ComputeSolutionFitness();
                fit_txtbox.Text = s.ToString();
            }

        }
    }

    public static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rnd = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static int[] GetRow(int[,] mtx, int rowNumber)
        {
            return Enumerable.Range(0, mtx.GetLength(1))
                    .Select(x => mtx[rowNumber, x])
                    .ToArray();
        }

        public static int[] GetColumn(int[,] mtx, int columnNumber)
        {
            return Enumerable.Range(0, mtx.GetLength(0))
                    .Select(x => mtx[x, columnNumber])
                    .ToArray();
        }

        public static int CountSize(int val)
        {
            int result = 0;
            for(int i = 0; i < val; i++)
            {
                result += (1 + i);
            }
            return result;
        }

        public static List<string[]> Find_Possibilities(int interruptions)
        {
            List<string[]> LL = new List<string[]>();
            int size = interruptions;
            for(int i = 0; i < CountSize(size); i++)
            {
                string[] arr = new string[size];
                for (int j = 0; j < size; j++)
                {
                    int num = (j + 1);
                    arr[j] = num.ToString();
                }
                LL.Add(arr);
            }

            bool flag = false;
            int block_counter = 1;
            int inblock_line_counter = 0;

            for (int i = 0; i < LL.Count; i++)
            {
                if (flag == false)
                {
                    if (i + 1 == size)
                    {
                        flag = true;
                    }
                    if (i == 0)
                    {
                        continue;
                    }
                    for(int j=0; j < i; j++)
                    {
                        LL[i][LL[i].Length - 1 - j] += '1';
                    }
                }
                else
                {
                    int block_blocker = size - block_counter;
                    while(inblock_line_counter < block_blocker)
                    {
                        for(int j = 0; j < block_counter; j++)
                        {
                            LL[i][j] += '0';
                        }
                        if (inblock_line_counter == 0)
                        {
                            inblock_line_counter += 1;
                            break;
                        }
                        if (inblock_line_counter > 0)
                        {
                            for(int j = 0; j < inblock_line_counter; j++)
                            {
                                LL[i][LL[i].Length - 1 - j] += '1';
                            }
                            inblock_line_counter += 1;
                            break;
                        }
                    }
                    if (inblock_line_counter == block_blocker)
                    {
                        block_counter += 1;
                        inblock_line_counter = 0;
                    }
                }
            }
            if(size > 19)
            {
                throw new InvalidOperationException("Za dużo przerw zeby wyliczyć");
            }
            if(size > 9)
            {
                foreach(var L in LL)  //Jesli mam 2 10 to -10 oznacza przed 1 a 10 oznacza 10 gap
                {
                    if(L.Count(s => s == "10") == 2)
                    {
                        int i = Array.IndexOf(L, "10");
                        L[i] = "-10";
                    }
                }
            }
            return LL;    
        }

        public static IDictionary<string, int>  Count_posibilities(int [] ones_indexes, List<string[]> pos)
        {
            IDictionary<string, int> costs = new Dictionary<string, int>();
            List<int> gaps_lengths = new List<int>();
            List<int[]> gaps_indexes = new List<int[]>();

            for (int i = 0; i < ones_indexes.Length - 1; i++)
            {
                if (ones_indexes[i + 1] != ones_indexes[i] + 1)
                {
                    gaps_lengths.Add(ones_indexes[i + 1] - ones_indexes[i] - 1);
                    gaps_indexes.Add(new int[] { ones_indexes[i] + 1, ones_indexes[i + 1] - 1 });
                }
            }
            HashSet<string> posibilities_unique = new HashSet<string>();
            foreach (var arr in pos)
            {
                posibilities_unique.UnionWith(arr);
            }
            if(posibilities_unique.AsQueryable().Any(v => v.Length == 3))
            {
                foreach (var val in posibilities_unique)
                {
                    if (val.Length == 1 || (val.Length == 2 && (int)char.GetNumericValue(val[0]) == 1))
                    {
                        costs.Add(val, gaps_lengths[int.Parse(val) - 1]);
                    }
                    else
                    {
                        var gap_num = (int)char.GetNumericValue(val[0]);
                        var aft_or_bef = (int)char.GetNumericValue(val[1]);
                        if (val[0] == '-')
                        {
                            gap_num = (int)char.GetNumericValue(val[1]);
                            aft_or_bef = (int)char.GetNumericValue(val[2]);
                        }
                        if(val.Length == 3 && val[0] != '-') 
                        { 
                            gap_num = int.Parse(val.Substring(0, 2));
                            aft_or_bef = (int)char.GetNumericValue(val[2]);
                        }
                        if (gap_num == gaps_lengths.Count && aft_or_bef == 1)
                        {
                            costs.Add(val, ones_indexes.Max() - gaps_indexes[gap_num - 1][1]);
                        }
                        else if (gap_num == 1 && aft_or_bef == 0)
                        {
                            costs.Add(val, gaps_indexes[0][0] - ones_indexes.Min());
                        }
                        else if (aft_or_bef == 1)
                        {
                            costs.Add(val, gaps_indexes[gap_num][0] - gaps_indexes[gap_num - 1][1] - 1);
                        }
                        else
                        { 
                         costs.Add(val, gaps_indexes[gap_num - 1][0] - gaps_indexes[gap_num - 2][1] - 1); 
                        }
                    }
                }
            }
            else
            {
                foreach (var val in posibilities_unique)
                {
                    if (val.Length == 1)
                    {
                        costs.Add(val, gaps_lengths[int.Parse(val) - 1]);
                    }
                    else
                    {
                        var gap_num = (int)char.GetNumericValue(val[0]);
                        var aft_or_bef = (int)char.GetNumericValue(val[1]);
                        if (gap_num == gaps_lengths.Count && aft_or_bef == 1)
                        {
                            costs.Add(val, ones_indexes.Max() - gaps_indexes[gap_num - 1][1]);
                        }
                        else if (gap_num == 1 && aft_or_bef == 0)
                        {
                            costs.Add(val, gaps_indexes[0][0] - ones_indexes.Min());
                        }
                        else if (aft_or_bef == 1)
                        {
                            costs.Add(val, gaps_indexes[gap_num][0] - gaps_indexes[gap_num - 1][1] - 1);
                        }
                        else
                        {
                        costs.Add(val, gaps_indexes[gap_num - 1][0] - gaps_indexes[gap_num - 2][1] - 1);
                        }
                    }
                }
            }
            return costs;
        }
    }
}
