using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

string[] lines = await File.ReadAllLinesAsync("passwords.txt");

List<PasswordValidator> passwords = lines.Select(x => {
    var result = x.Split(':');
    var valid = result[0].Split(' ');
    var limits = valid[0].Split('-');
    return new PasswordValidator() {
        min = int.Parse(limits[0]),
        max = int.Parse(limits[1]),
        letter = char.Parse(valid[1]),
        password = result[1].Substring(1)
    };
}).ToList();

ValidPasswordsTask1(passwords);
ValidPasswordsTask2(passwords);

static void ValidPasswordsTask1(List<PasswordValidator> passwords) {
    var valid = passwords.FindAll(x => {
        int letterCount = x.password.Count(passChar => passChar == x.letter);
        return letterCount >= x.min && letterCount <= x.max;
    });

    Console.WriteLine("Total Valid Passwords: {0}", valid.Count);
}

static void ValidPasswordsTask2(List<PasswordValidator> passwords) {
    var valid = passwords.FindAll(x => {
        bool firstValid = x.password[x.min-1] == x.letter;
        bool secondValid = x.password[x.max-1] == x.letter;
        return (firstValid || secondValid) && (firstValid != secondValid);
    });

    Console.WriteLine("Total Valid Passwords: {0}", valid.Count);
}

class PasswordValidator {
    public int min;
    public int max;
    public char letter;
    public string password;
}