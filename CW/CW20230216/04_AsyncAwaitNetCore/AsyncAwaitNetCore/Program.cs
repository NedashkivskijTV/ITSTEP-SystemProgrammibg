using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HelloApp
{
    class Program
    {
        static async Task ReadWriteAsync(string s, string name)
        {
            Console.WriteLine($"Початок методу ReadWriteAsync поток ID = {Thread.CurrentThread.ManagedThreadId}\n");
            
            using (StreamWriter writer = new StreamWriter(name, false))
            {
                Console.WriteLine($"StreamWriter до запису в файл поток ID = {Thread.CurrentThread.ManagedThreadId}\n");
                
                await writer.WriteLineAsync(s);  // асинхронная запись в файл
                
                Console.WriteLine($"StreamWriter після запису в файл поток ID = {Thread.CurrentThread.ManagedThreadId}\n");
            }

            using (StreamReader reader = new StreamReader(name))
            {
                Console.WriteLine($"StreamReader до читання з файлу поток ID = {Thread.CurrentThread.ManagedThreadId}\n");
                
                string result = await reader.ReadToEndAsync();  // асинхронное чтение из файла
                Console.WriteLine(result);
                
                Console.WriteLine($"StreamReader після читання з файлу поток ID = {Thread.CurrentThread.ManagedThreadId}\n");
            }
            
            Console.WriteLine($"Кінець методу ReadWriteAsync поток ID = {Thread.CurrentThread.ManagedThreadId}\n");
        }

        static async Task Main(string[] args)
        {
            // hello.txt - файл, который будет записываться и считываться
            string name = "hello.txt";
            string s =
@"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Consequat interdum varius sit amet mattis vulputate enim nulla. Nunc mattis enim ut tellus. Leo duis ut diam quam nulla porttitor massa id neque. Leo in vitae turpis massa sed elementum. Dictum fusce ut placerat orci. Dignissim diam quis enim lobortis scelerisque. Ornare arcu odio ut sem nulla pharetra diam sit amet. Posuere sollicitudin aliquam ultrices sagittis. Tortor posuere ac ut consequat semper viverra nam. Nisl pretium fusce id velit ut. Orci nulla pellentesque dignissim enim sit. Platea dictumst quisque sagittis purus sit amet volutpat consequat mauris.
Sodales neque sodales ut etiam sit amet nisl purus.Risus in hendrerit gravida rutrum.Elementum sagittis vitae et leo.Tellus mauris a diam maecenas sed enim ut sem viverra. Blandit massa enim nec dui.Faucibus nisl tincidunt eget nullam non. Quam viverra orci sagittis eu volutpat odio.Vitae ultricies leo integer malesuada nunc vel.Viverra tellus in hac habitasse platea dictumst vestibulum.Metus aliquam eleifend mi in nulla posuere sollicitudin aliquam. Et malesuada fames ac turpis egestas integer eget aliquet.Vulputate sapien nec sagittis aliquam malesuada bibendum arcu vitae elementum.
Arcu risus quis varius quam quisque id.Diam volutpat commodo sed egestas.Commodo ullamcorper a lacus vestibulum sed arcu.Tristique magna sit amet purus gravida. Ullamcorper sit amet risus nullam eget felis eget nunc.Fermentum iaculis eu non diam phasellus vestibulum.Nisi scelerisque eu ultrices vitae auctor eu augue ut.Tellus id interdum velit laoreet.Sed libero enim sed faucibus turpis. Convallis tellus id interdum velit laoreet id.At in tellus integer feugiat scelerisque varius morbi.
Sit amet nisl purus in mollis nunc sed id semper.Id venenatis a condimentum vitae sapien pellentesque habitant. Morbi enim nunc faucibus a pellentesque. Augue lacus viverra vitae congue eu consequat.Nunc id cursus metus aliquam eleifend mi in nulla posuere. Ut eu sem integer vitae justo eget magna fermentum iaculis. In vitae turpis massa sed elementum tempus egestas sed.Sit amet mauris commodo quis imperdiet. Sit amet nulla facilisi morbi tempus. Integer feugiat scelerisque varius morbi.Quis varius quam quisque id diam. Aliquam sem fringilla ut morbi tincidunt augue interdum. Sit amet dictum sit amet justo donec enim diam vulputate. Sit amet nulla facilisi morbi tempus iaculis urna id.Adipiscing commodo elit at imperdiet dui accumsan sit. Dictum sit amet justo donec enim diam.
Nulla facilisi morbi tempus iaculis urna. Lacus suspendisse faucibus interdum posuere lorem ipsum dolor sit.Varius duis at consectetur lorem.Urna nunc id cursus metus aliquam eleifend.Imperdiet nulla malesuada pellentesque elit.Vivamus at augue eget arcu dictum varius duis. Ut diam quam nulla porttitor.Ut eu sem integer vitae justo eget magna fermentum iaculis. Blandit cursus risus at ultrices mi tempus imperdiet. Nam libero justo laoreet sit amet cursus sit amet.Donec pretium vulputate sapien nec sagittis aliquam malesuada bibendum arcu. Amet nisl purus in mollis.Integer eget aliquet nibh praesent tristique magna sit amet.Feugiat pretium nibh ipsum consequat nisl vel pretium. Cursus in hac habitasse platea dictumst quisque sagittis purus sit. Tristique nulla aliquet enim tortor at.
Ac odio tempor orci dapibus ultrices in iaculis.Sit amet nisl suscipit adipiscing bibendum. In fermentum posuere urna nec tincidunt praesent semper. Eu turpis egestas pretium aenean.In dictum non consectetur a.Nullam eget felis eget nunc.Eu feugiat pretium nibh ipsum consequat nisl vel pretium lectus. Enim ut tellus elementum sagittis vitae et leo. Arcu ac tortor dignissim convallis aenean et tortor at risus. Egestas maecenas pharetra convallis posuere morbi leo urna molestie at. Sem viverra aliquet eget sit.Pretium fusce id velit ut tortor pretium.
Varius duis at consectetur lorem donec. Fermentum et sollicitudin ac orci phasellus egestas tellus rutrum.Sit amet justo donec enim.Pulvinar neque laoreet suspendisse interdum consectetur. Pharetra diam sit amet nisl suscipit adipiscing bibendum est.Vitae nunc sed velit dignissim sodales ut eu sem integer. Ullamcorper a lacus vestibulum sed arcu non odio. Purus in massa tempor nec feugiat nisl pretium. Diam sollicitudin tempor id eu.Pharetra et ultrices neque ornare aenean euismod elementum nisi.Eget nunc scelerisque viverra mauris in aliquam.Ultrices vitae auctor eu augue ut lectus arcu.";

            Console.WriteLine($"Головний потік ID до ReadWriteAsync = {Thread.CurrentThread.ManagedThreadId}\n");

            await ReadWriteAsync(s, name);

            Console.WriteLine($"Головний потік ID після ReadWriteAsync = {Thread.CurrentThread.ManagedThreadId}\n");

            Console.WriteLine("Обробка тексту завершена!!!\n\n");
            Console.Read();
        }
    }
}