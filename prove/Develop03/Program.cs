using System;
using System.Net;
using System.Threading.Tasks.Dataflow;

class Program
{
    static void Main(string[] args)
    {
        List<Reference> scriptureLibrary =
        [
            new Reference("John","3",[16],"For God so loved the world that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
            new Reference("Proverbs","3","5-6",[5,6],"5 Trust in the Lord with all thine heart; and lean not unto thine own understanding. 6 In all thy ways acknowledge him, and he shall direct thy paths."),
            new Reference("1 Nephi","3",[7],"And it came to pass that I, Nephi, said unto my father: I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them."),
            new Reference("2 Nephi","2",[25],"Adam fell that men might be; and men are, that they might have joy."),
            new Reference("James","1","5-6",[5,6],"5 If any of you lack wisdom, let him ask of God, that giveth to all men liberally, and upbraideth not; and it shall be given him. 6 But let him ask in faith, nothing wavering. For he that wavereth is like a wave of the sea driven with the wind and tossed."),
            new Reference("Matthew","22","36-39",[36,37,38,39],"36 Master, which is the great commandment in the law? 37 Jesus said unto him, Thou shalt love the Lord thy God with all thy heart, and with all thy soul, and with all thy mind. 38 This is the first and great commandment. 39 And the second is like unto it, Thou shalt love thy neighbor as thyself."),
            new Reference("Luke","2","10-12",[10,11,12],"10 And the angel said unto them, Fear not: for, behold, I bring you good tidings of great joy, which shall be to all people. 11 For unto you is born this day in the city of David a Saviour, which is Christ the Lord. 12 And this shall be a sign unto you; Ye shall find the babe wrapped in swaddling clothes, lying in a manger."),
            new Reference("Isaiah","1",[18],"Come now, and let us reason together, saith the Lord: though your sins be as scarlet, they shall be as white as snow; though they be red like crimson, they shall be as wool."),
            new Reference("Joshua","24",[15],"And if it seem evil unto you to serve the Lord, choose you this day whom ye will serve; whether the gods which your fathers served that were on the other side of the flood, or the gods of the Amorites, in whose land ye dwell: but as for me and my house, we will serve the Lord."),
            new Reference("Malachi","4","5-6",[5,6],"5 Behold, I will send you Elijah the prophet before the coming of the great and dreadful day of the Lord: 6 And he shall turn the heart of the fathers to the children, and the heart of the children to their fathers, lest I come and smite the earth with a curse."),
            new Reference("Malachi","3","8-10",[8,9,10],"8 Will a man rob God? Yet ye have robbed me. But ye say, Wherein have we robbed thee? In tithes and offerings. 9 Ye are cursed with a curse: for ye have robbed me, even this whole nation. 10 Bring ye all the tithes into the storehouse, that there may be meat in mine house, and prove me now herewith, saith the Lord of hosts, if I will not open you the windows of heaven, and pour you out a blessing, that there shall not be room enough to recieve it."),
        ];
        Random number = new Random();
        int scriptureIndex = number.Next(0,scriptureLibrary.Count);
        Reference scripture = scriptureLibrary[scriptureIndex];
        Console.Clear();
        Console.WriteLine(scripture);
        Console.Write("\nPress enter to continue or type 'quit' to finish:");
        string response = Console.ReadLine();
        while (response != "quit") {
            Console.Clear();
            scripture.HideWords();
            scripture.PrintPartialScripture();
            Console.Write("\nPress enter to continue or type 'quit' to finish:");
            response = Console.ReadLine();
        }
    }
}