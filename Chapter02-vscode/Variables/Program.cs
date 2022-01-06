

object height = 1.88;
object name = "Amir";
Console.WriteLine($"{name} is {height} meters tall.");

int length =  ((string)name).Length;
Console.WriteLine($"{name} has {length} characters.");

/*
• L: infers long
• UL: infers ulong
• M: infers decimal
• D: infers double
• F: infers float
*/

Console.WriteLine($"default(int) = {default(int)}");
Console.WriteLine($"default(bool) = {default(bool)}");
Console.WriteLine($"default(DateTime) = {default(DateTime)}");
Console.WriteLine($"default(string) = {default(string)}");


string[] names;
names = new string[4];
names[0] = "Kate";
names[1] = "Jack";
names[2] = "Rebecca";
names[3] = "Tom";

for (int i = 0; i < names.Length; i++)
{
    Console.WriteLine(names[i]);
}