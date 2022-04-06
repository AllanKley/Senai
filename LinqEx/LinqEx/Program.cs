using System.Collections.Generic;
using System.Linq;
using static System.Console;




Universidade uni = new Universidade();






//("Os departamentos, em ordem alfabética, com o número de disciplinas.").Print();
//uni.Pesquisa1();
//WriteLine();

//("Liste os alunos e suas idades com seus respectivos professores.").Print();
//uni.Pesquisa2();
//WriteLine();

//("Liste os professores e seus salários com seus respectivos alunos.").Print();
//uni.Pesquisa3();
//WriteLine();

//("Top 10 Professores com mais alunos da universidade.").Print();
//uni.Pesquisa4();
//WriteLine();

("Considerando que todo aluno custa 300 reais mais o salário dos seus professores" +
    " divido entre seus colegas de classe. Liste os alunos e seus respectivos custos.").Print();
uni.Pesquisa5();
WriteLine();

ReadKey(true);

static class ShowExtension
{
    static int _count = 0;
    public static void Print(this string s)
        => WriteLine($"Pesquisa {++_count}. {s}\n");
}
static class Pesquisador
{
    /// <summary>
    /// Os departamentos, em ordem alfabética, com o número de disciplinas.
    /// </summary>
    public static void Pesquisa1(this Universidade uni)
    {
        var Pesquisa = uni.Departamentos
            .Join(uni.Disciplinas, x => x.ID, y => y.DepartamentoID, (x, y) => new
            {
                ID = x.ID,
                NomeDepartamento = x.Nome
            })
            .GroupBy(x => x.ID)
            .Select(g => new
            {
                NomeDep = g.First().NomeDepartamento,
                QtdDisc = g.Count(),
            })
            .OrderBy(x => x.NomeDep);




        WriteLine("Departamento \tQtd Discplinas\n");
        foreach(var item in Pesquisa)
        {
            WriteLine(item.NomeDep + "\t\t" + item.QtdDisc);
        }
    }





    /// <summary>
    /// Liste os alunos com seus respectivos professores
    /// </summary>
    public static void Pesquisa2(this Universidade uni)
    {
        var Pesquisa = uni.Alunos
            .Select(x => new
            {
                Nome = x.Nome,
                Idade = x.Idade,
                Turma = x.TurmasMatriculados.Join(uni.Turmas, t => t, x => x.ID, (t,x) => new
                {
                    ProfessorId = x.ProfessorID
                }).Join(uni.Professores, x => x.ProfessorId, y => y.ID, (x,y) => new
                {
                    NomeProfessor = y.Nome
               })
            });



        
        foreach (var aluno in Pesquisa)
        {
            WriteLine("\n-------------------------------------------------------------------------------------");
            WriteLine("\nAluno\t\tIdade");
            WriteLine(aluno.Nome.Split(" ").First() + "\t\t" + aluno.Idade);
            WriteLine("\n---------------------------------");
            WriteLine("\nProfessores:\n");
            foreach(var turma in aluno.Turma)
            {
                WriteLine("\t" + turma.NomeProfessor);
            }
        }
    }





    /// <summary>
    /// Liste o número de alunos que cada professor possui.
    /// </summary>
    public static void Pesquisa3(this Universidade uni)
    {
        var Pesquisa = uni.Professores
            .Join(uni.Turmas, x => x.ID, y => y.ProfessorID, (x, y) => new
            {
                NomeProfessor = x.Nome,
                contador = uni.Alunos.Where(x => x.TurmasMatriculados.Contains(y.ID)).Count()
            }).GroupBy(x => x.NomeProfessor);


        foreach (var professor in Pesquisa)
        {
            WriteLine(professor.First().NomeProfessor.Split(" ").First() + "\t-    " + professor.First().contador);
        }
    }





    /// <summary>
    /// Top 10 Professores com mais alunos da universidade
    /// </summary>
    public static void Pesquisa4(this Universidade uni)
    {
        var result = uni.Professores.Join(uni.Turmas, x => x.ID, t => t.ProfessorID, (x, t) => new
        {
            Nome = x.Nome,
            QtdAlunos = uni.Alunos.Count(y => y.TurmasMatriculados.Contains(t.ID))
        }).OrderByDescending(x => x.QtdAlunos).GroupBy(x => x.Nome).Take(10);

        Console.WriteLine("Professor\tQtd Alunos\n");
        foreach (var item in result)
        {
            Console.WriteLine(item.First().Nome.Split(" ").First() + " \t\t " + item.First().QtdAlunos);
        }
    }





    /// <summary>
    /// Considerando que todo aluno custa 300 reais mais o salário dos seus professores
    /// divido entre seus colegas de classe. Liste os alunos e seus respectivos custos
    /// </summary>
    public static void Pesquisa5(this Universidade uni)
    {
        var Pesquisa = uni.Alunos
            .Select(x => new
            {
                Nome = x.Nome,
                Turmas = x.TurmasMatriculados.Join(uni.Turmas, t => t, x => x.ID, (t, x) => new
                {
                    Id = x.ID,
                    ProfessorID = x.ProfessorID,

                })
                .Join(uni.Professores, a => a.ProfessorID, y => y.ID, (a, y) => new
                {
                    SalarioProfessor = y.Salario,
                    QtdAlunos = uni.Alunos.Where(x => x.TurmasMatriculados.Contains(a.Id)).Count(),
                })
                .Select(x => new
                {
                    CustoTotal = x.SalarioProfessor / x.QtdAlunos
                })
             }).Select(x => new
             {
                 NomeAluno = x.Nome,
                 CustoTotal = 300 + (x.Turmas.Sum(y => y.CustoTotal)),
             });


        foreach (var Aluno in Pesquisa)
        {
            Console.WriteLine($"Nome: {Aluno.NomeAluno.Split(" ").First()}\nCusto: {Aluno.CustoTotal.ToString("R$ 00.00")}");


            WriteLine();
        }

    }
}

public class Professor
{
    public int ID { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public int DepartamentoID { get; set; }
    public decimal Salario { get; set; }
}

public class Departamento
{
    public int ID { get; set; }
    public string Nome { get; set; }
}

public class Disciplina
{
    public int ID { get; set; }
    public int DepartamentoID { get; set; }
    public string Nome { get; set; }
}

public class Turma
{
    public int ID { get; set; }
    public int DisciplinaID { get; set; }
    public int ProfessorID { get; set; }
    public string Codigo { get; set; }
}

public class Aluno
{
    public int ID { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public List<int> TurmasMatriculados { get; set; } = new List<int>();
}

public class Universidade
{
    public IEnumerable<Departamento> Departamentos { get; set; } = new List<Departamento>()
    {
        new Departamento() { ID = 1, Nome = "DAMAT" },
        new Departamento() { ID = 2, Nome = "DAFIS" },
        new Departamento() { ID = 3, Nome = "DAINF" },
        new Departamento() { ID = 4, Nome = "DAELN" }
    };

    public IEnumerable<Disciplina> Disciplinas { get; set; } = new List<Disciplina>()
    {
        new Disciplina() { ID = 1, Nome = "Cálculo 1", DepartamentoID = 1 },
        new Disciplina() { ID = 2, Nome = "Cálculo 2", DepartamentoID = 1 },
        new Disciplina() { ID = 3, Nome = "Cálculo 3", DepartamentoID = 1 },
        new Disciplina() { ID = 4, Nome = "Física 1", DepartamentoID = 2 },
        new Disciplina() { ID = 5, Nome = "Física 2", DepartamentoID = 2 },
        new Disciplina() { ID = 6, Nome = "Física 3", DepartamentoID = 2 },
        new Disciplina() { ID = 7, Nome = "Técnicas de Programação 1", DepartamentoID = 3},
        new Disciplina() { ID = 8, Nome = "Técnicas de Programação 2", DepartamentoID = 3},
        new Disciplina() { ID = 9, Nome = "Eletricidade", DepartamentoID = 4 },
        new Disciplina() { ID = 10, Nome = "Oficinas de Integração", DepartamentoID = 4 },
        new Disciplina() { ID = 11, Nome = "Estrutura de Dados 2", DepartamentoID = 3 },
        new Disciplina() { ID = 12, Nome = "Física 4", DepartamentoID = 2 }
    };

    public IEnumerable<Professor> Professores { get; set; } = new List<Professor>()
    {
        new Professor() { ID = 1, DepartamentoID = 1, Nome = "Fábio Dorini", Idade = 34, Salario = 11000 },
        new Professor() { ID = 2, DepartamentoID = 1, Nome = "Inácio", Idade = 45, Salario = 7000 },
        new Professor() { ID = 3, DepartamentoID = 1, Nome = "Roni", Idade = 38, Salario = 12000 },
        new Professor() { ID = 4, DepartamentoID = 3, Nome = "Leiza Dorini", Idade = 30, Salario = 8000 },
        new Professor() { ID = 5, DepartamentoID = 2, Nome = "Rafael Barreto", Idade = 32, Salario = 15000 },
        new Professor() { ID = 6, DepartamentoID = 4, Nome = "Jean Simão", Idade = 38, Salario = 9600 },
        new Professor() { ID = 7, DepartamentoID = 4, Nome = "Razera", Idade = 29, Salario = 13000 },
        new Professor() { ID = 8, DepartamentoID = 4, Nome = "Cezar Sanchez", Idade = 31, Salario = 8500 },
        new Professor() { ID = 9, DepartamentoID = 3, Nome = "Bogdan Nassu", Idade = 33, Salario = 14000 },
        new Professor() { ID = 10, DepartamentoID = 3, Nome = "Bogado", Idade = 48, Salario = 5000 }
    };

    public IEnumerable<Turma> Turmas { get; set; } = new List<Turma>()
    {
        new Turma() { ID = 1, Codigo = "S71", DisciplinaID = 1, ProfessorID = 1 },
        new Turma() { ID = 2, Codigo = "S77", DisciplinaID = 1, ProfessorID = 1 },
        new Turma() { ID = 3, Codigo = "S71", DisciplinaID = 2, ProfessorID = 2 },
        new Turma() { ID = 4, Codigo = "S71", DisciplinaID = 3, ProfessorID = 3 },
        new Turma() { ID = 5, Codigo = "S71", DisciplinaID = 4, ProfessorID = 5 },
        new Turma() { ID = 6, Codigo = "S71", DisciplinaID = 5, ProfessorID = 5 },
        new Turma() { ID = 7, Codigo = "S71", DisciplinaID = 6, ProfessorID = 5 },
        new Turma() { ID = 8, Codigo = "S71", DisciplinaID = 7, ProfessorID = 4 },
        new Turma() { ID = 9, Codigo = "S77", DisciplinaID = 7, ProfessorID = 9 },
        new Turma() { ID = 10, Codigo = "S71", DisciplinaID = 8, ProfessorID = 6 },
        new Turma() { ID = 11, Codigo = "S77", DisciplinaID = 8, ProfessorID = 10 },
        new Turma() { ID = 12, Codigo = "S71", DisciplinaID = 9, ProfessorID = 7 },
        new Turma() { ID = 13, Codigo = "S73", DisciplinaID = 9, ProfessorID = 7 },
        new Turma() { ID = 14, Codigo = "S71", DisciplinaID = 10, ProfessorID = 8 },
        new Turma() { ID = 15, Codigo = "S73", DisciplinaID = 11, ProfessorID = 4 },
        new Turma() { ID = 16, Codigo = "S73", DisciplinaID = 11, ProfessorID = 10 },
        new Turma() { ID = 17, Codigo = "S71", DisciplinaID = 12, ProfessorID = 5 }
    };

    public IEnumerable<Aluno> Alunos { get; set; } = new List<Aluno>()
    {
        new Aluno() { ID = 1, TurmasMatriculados = new List<int>() { 16, 14, 13, 10, 9, 3, 6 },
            Nome = "Leonardo Trevisan Silio", Idade = 18},
        new Aluno() { ID = 2, TurmasMatriculados = new List<int>() { 15, 14, 11, 8, 3, 6, 9 },
            Nome = "Carol Rosa", Idade = 18},
        new Aluno() { ID = 3, TurmasMatriculados = new List<int>() { 16, 14, 9, 6, 3, 7, 1, 2 },
            Nome = "Bruna Pinheirinho", Idade = 14},
        new Aluno() { ID = 4, TurmasMatriculados = new List<int>() { 15, 12, 9, 5, 2, 7, 4 },
            Nome = "Tiago Sendeski", Idade = 21},
        new Aluno() { ID = 5, TurmasMatriculados = new List<int>() { 1, 2, 3, 4, 5, 6, 8 },
            Nome = "Ian Douglas", Idade = 23},
        new Aluno() { ID = 6, TurmasMatriculados = new List<int>() { 17, 16, 15, 14, 13, 12, 11 },
            Nome = "Jordão Vyctor", Idade = 22},
        new Aluno() { ID = 7, TurmasMatriculados = new List<int>() { 1, 8, 15, 7, 13, 3, 11, 4 },
            Nome = "Alan Jun Onoda", Idade = 18},
        new Aluno() { ID = 7, TurmasMatriculados = new List<int>() {4, 13, 5, 6 , 17, 11, 13, 9 },
            Nome = "Wagber", Idade = 19},
        new Aluno() { ID = 7, TurmasMatriculados = new List<int>() { 3, 2, 1, 17, 5, 4, 6, 7, 8, 14 },
            Nome = "Vitor Corra", Idade = 18},
        new Aluno() { ID = 7, TurmasMatriculados = Enumerable.Range(1, 12).ToList(),
            Nome = "Gabriel Maia", Idade = 18}
    };
}
