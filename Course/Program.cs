using System;
using System.Linq;
using Course.Entities;
using System.Collections.Generic;

namespace Course
{
    class Program
    {
        
        static void Print<T>(string message, IEnumerable<T> collection) // Método auxiliar criado para imprimir as novas 'Coleções' criadas e para exibí-las
        {
            Console.WriteLine(message);
            foreach(T obj in collection)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine();
        }
        
        static void Main(string[] args)
        {
            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Electronics", Tier = 1 };

            List<Product> products = new List<Product>()
            {
                new Product() { Id = 1, Name = "Computer", Price = 1100.0, Category = c2 },
                new Product() { Id = 2, Name = "Hammer", Price = 90.0, Category = c1 },
                new Product() { Id = 3, Name = "TV", Price = 1700.0, Category = c3 },
                new Product() { Id = 4, Name = "Notebook", Price = 1300.0, Category = c2 },
                new Product() { Id = 5, Name = "Saw", Price = 80.0, Category = c1 },
                new Product() { Id = 6, Name = "Tablet", Price = 700.0, Category = c2 },
                new Product() { Id = 7, Name = "Camera", Price = 700.0, Category = c3 },
                new Product() { Id = 8, Name = "Printer", Price = 350.0, Category = c3 },
                new Product() { Id = 9, Name = "MacBook", Price = 1800.0, Category = c2 },
                new Product() { Id = 10, Name = "Sound Bar", Price = 700.0, Category = c3 },
                new Product() { Id = 11, Name = "Level", Price = 70.0, Category = c1 }
            };

            //var r1 = products.Where(p => p.Category.Tier == 1 && p.Price < 900.0);
            var r1 =
                from p in products
                where p.Category.Tier == 1 && p.Price < 900.0
                select p; // Produtos da Categoria 1 cujo Preço for menor que 900.0 - Sintaxe SQL
            Print("TIER 1 AND PRICE < 900:", r1);

            //var r2 = products.Where(p => p.Category.Name == "Tools").Select(p => p.Name);
            var r2 =
                from p in products
                where p.Category.Name == "Tools"
                select p.Name; // NOMES dos Produtos da Categoria "Tools(Ferramentas)" - Sintaxe SQL
            Print("NAMES OF PRODUCTS FROM TOOLS:", r2);

            //var r3 = products.Where(p => p.Name[0] == 'C').Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name });
            var r3 =
                from p in products
                where p.Name[0] == 'C'
                select new {
                    p.Name,
                    p.Price,
                    CategoryName = p.Category.Name
                }; // Mais de um campo com o mesmo nome (nesse caso é o 'NAME'), pelo menos um deles é preciso ser 'Nomeado' com um 'Apelido' - Sintaxe SQL
            Print("NAMES STARTED WITH 'C' AND ANONYMOUS OBJECT", r3);

            //var r4 = products.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);
            var r4 =
                from p in products
                where p.Category.Tier == 1
                orderby p.Name // Este aparece em SEGUNDO
                orderby p.Price // Este aparece PRIMEIRO
                select p; // Coleção de Produtos da Categoria '1', Ordernado por 'Price', e então por 'Name' - Sintaxe SQL; OBS: No caso dessa Sintaxe, a ordem é reversa quando se usa o 'orderby'
            Print("TIER 1 ORDER BY PRICE THEN BY NAME", r4);

            //var r5 = r4.Skip(2).Take(4);
            var r5 =
                (from p in r4
                 select p).Skip(2).Take(4); // Aproveitando a Coleção 'r4', Pule 2 Objetos e Pegue 4 - Sintaxe SQL; OBS: O 'from' e o 'select' são colocados em Parênteses e depois é colocado o 'finalizador('Skip' e 'Take' neste caso)'
            Print("TIER 1 ORDER BY PRICE THEN BY NAME SKIP 2 TAKE 4", r5);

            //var r6 = products.FirstOrDefault();
            var r6 =
                (from p in products select p)
                .FirstOrDefault(); // Pega e retorna o primeiro Elemento ou retorna 'null' se a Coleção for 'vazia' - Sintaxe SQL; OBS: Este método seria mais 'longo', por este ângulo, o método anterior seria melhor
            Console.WriteLine("First or default Test1: " + r6);

            //var r7 = products.Where(p => p.Price > 3000.0).FirstOrDefault();
            var r7 =
                (from p in products
                 where p.Price > 3000.0
                 select p)
                .FirstOrDefault(); // Uma Expressão que propositalmente causa 'Erro', então é usado o 'FirstOrDefault' para retornar 'null' caso a coleção não retorne nada - Sintaxe SQL
            Console.WriteLine("First or default Test2: " + r7);
            Console.WriteLine();

            //var r8 = products.Where(p => p.Id == 3).SingleOrDefault();
            var r8 =
                (from p in products
                 where p.Id == 3
                 select p)
                .SingleOrDefault(); // Pesquisas por 'Id' geralmente retornam ou apenas um elemento ou nada, nessas ocasiões é necessário usar o 'SingleOrDefault' - Sintaxe SQL
            Console.WriteLine("Single or default Test1: " + r8);
            //var r9 = products.Where(p => p.Id == 30).SingleOrDefault();
            var r9 =
                (from p in products
                 where p.Id == 30
                 select p)
                .SingleOrDefault(); // Quando a pesquisa não retorna nada, o 'SingleOrDefault' retorna 'null' sem ocorrer EXCEÇÕES - Sintaxe SQL
            Console.WriteLine("Single or default Test2: " + r9);
            Console.WriteLine();

            // Operações de Agregação
            //var r10 = products.Max(p => p.Price);
            var r10 =
                (from p in products
                 select p)
                 .Max(p => p.Price); // Retira o valor Máximo de uma Coleção que DEVE ser especificada
            Console.WriteLine("Max price: " + r10);
            //var r11 = products.Min(p => p.Price);
            var r11 =
                (from p in products
                 select p)
                 .Min(p => p.Price); // Retira o valor Mínimo de uma Coleção que DEVE ser especificada
            Console.WriteLine("Min price: " + r11);
            //var r12 = products.Where(p => p.Category.Id == 1).Sum(p => p.Price);
            var r12 =
                (from p in products
                 where p.Category.Id == 1
                 select p)
                 .Sum(p => p.Price); // Soma de todos os PREÇOS (o valor deve ser especificado) de cada produto especificado (no caso do filtro 'Where') - Sintaxe SQL
            Console.WriteLine("Category 1 Sum prices: " + r12);
            //var r13 = products.Where(p => p.Category.Id == 1).Average(p => p.Price);
            var r13 =
                (from p in products
                 where p.Category.Id == 1
                 select p)
                 .Average(p => p.Price); // Média dos Preços dos Produtos especificados - Sintaxe SQL
            Console.WriteLine("Category 1 Average prices: " + r13);
            //var r14 = products.Where(p => p.Category.Id == 5).Select(p => p.Price).DefaultIfEmpty(0.0).Average();
            var r14 =
                (from p in products
                 where p.Category.Id == 5
                 select p.Price)
                .DefaultIfEmpty(0.0)
                .Average(); // Em um caso de ser feito uma condição que não retorne nada é usado o 'DefaultIfEmpty' retornando um valor especificdo caso não acabe retornando nada - Sintaxe SQL
            Console.WriteLine("Category 5 Average prices: " + r14); // Neste caso o Average não tem valor especificado porque isso já foi feito no 'Select'
            //var r15 = products.Where(p => p.Category.Id == 1).Select(p => p.Price).Aggregate(0.0, (x, y) => x + y);
            var r15 =
                (from p in products
                 where p.Category.Id == 1
                 select p.Price)
                .Aggregate(0.0, (x, y) => x + y); // Usando o 'Aggregate' e o 'Select' para Implementar uma soma Personalizada - Sintaxe SQL
            Console.WriteLine("Category 1 Aggregate sum: " + r15); // Se a Operação não retornar nada, o número 0.0 é usado na operação acima para prevenir este erro
            Console.WriteLine();

            //var r16 = products.GroupBy(p => p.Category);
            var r16 =
                from p in products
                group p by p.Category; // Operação de Agrupamento, neste caso baseado em critério de 'Category', agrupando-os primeiro pela 'chave' (no caso, Category) e depois os elementos de tal Tipo (no caso, Product) - Sintaxe SQL
            foreach (IGrouping<Category, Product> group in r16) // Lendo usando o 'IGrouping' primeiramente pelo tipo 'chave'(Category), cada elemento chamado de 'group', e depois pela 'coleção'
            {
                Console.WriteLine("Category " + group.Key.Name + ":");
                foreach (Product p in group) // Outro 'foreach' lendo, desta vez, lendo cada 'Produto(Product)', contido em cada 'group'
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine();
            }
        }
    }
}
