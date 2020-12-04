using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

string[] lines = await File.ReadAllLinesAsync("passports.txt");

List<Passport> passports = new List<Passport>();
Dictionary<string, string> passportValues = new Dictionary<string, string>();

for(int passportIndex = 0; passportIndex < lines.Length+1; passportIndex++){
    string line = passportIndex >= lines.Length ? "" : lines[passportIndex];
    if(string.IsNullOrWhiteSpace(line)){
        if(passportValues.Count <= 0) continue;
        
        string birthYear = passportValues.GetValueOrDefault("byr");
        string issueYear = passportValues.GetValueOrDefault("iyr");
        string expirationYear = passportValues.GetValueOrDefault("eyr");

        string passportId = passportValues.GetValueOrDefault("pid");
        string countryId = passportValues.GetValueOrDefault("cid");
        long pid = 0;
        long cid = 0;
        long.TryParse(passportId, out pid);
        long.TryParse(countryId, out cid);

        Passport currentPassport = new Passport(){
            birthYear = !string.IsNullOrEmpty(birthYear) ? int.Parse(birthYear) : 0,
            issueYear = !string.IsNullOrEmpty(issueYear) ? int.Parse(issueYear) : 0,
            expirationYear = !string.IsNullOrEmpty(expirationYear) ? int.Parse(expirationYear) : 0,

            height = passportValues.GetValueOrDefault("hgt"),
            hairColor = passportValues.GetValueOrDefault("hcl"),
            eyeColor = passportValues.GetValueOrDefault("ecl"),

            passportId = pid,
            countryId = cid
        };

        passports.Add(currentPassport);
        passportValues.Clear();
        continue;
    }

    string[] keyArray = line.Split(" ");
    for(int valueIndex = 0; valueIndex < keyArray.Length; valueIndex++){
        string[] keyValue = keyArray[valueIndex].Split(":");
        passportValues.Add(keyValue[0], keyValue[1]);
    }
}

List<Passport> passportstens = passports.Where(x => x.passportId.ToString().Length == 10).ToList();
Console.WriteLine("Passports with 10 numbers: {0}", passportstens.Count);
Console.WriteLine("Valid Passports: {0}", passports.Count(x => x.IsValid));

struct Passport {

    // Years in Numbers (2001)
    public int birthYear { get; set; }
    public int issueYear { get; set; }
    public int expirationYear { get; set; }

    // Colors in HEX or short name
    public string height { get; set; }
    public string hairColor { get; set; }
    public string eyeColor { get; set; }

    // Ids
    public long passportId { get; set; }
    public long? countryId { get; set; }
    public bool IsValid { get { 
        int inchHeight = 0;
        int cmHeight = 0;
        if(!string.IsNullOrEmpty(height)){
            if(height.Contains("cm")) int.TryParse(height.Substring(0, height.Length-2), out cmHeight);
            else if(height.Contains("in")) int.TryParse(height.Substring(0, height.Length-2), out inchHeight);
        }

        bool hairValid = string.IsNullOrEmpty(hairColor) ? false : 
            hairColor[0] == '#' && hairColor.Substring(1).All(x => "0123456789abcdef".Contains(x)) && hairColor.Length == 7
        ;

        string[] eyeColors = new string[]{"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

        bool birthYearValid = birthYear >= 1920 && birthYear <= 2020;
        bool issueYearValid = issueYear >= 2010 && issueYear <= 2020;
        bool expiryYearValid = expirationYear >= 2020 && expirationYear <= 2030;
        bool heightValid = 
                (!string.IsNullOrEmpty(height) ? (height.Contains("cm") ? cmHeight >= 150 && cmHeight <= 193 : true) : false) &&
                (!string.IsNullOrEmpty(height) ? (height.Contains("in") ? inchHeight >= 59 && inchHeight <= 76 : true) : false);
        bool eyeColorsValid = eyeColors.Contains(eyeColor);
        bool passportValid = passportId.ToString("D9").Length == 9;

        return birthYearValid && issueYearValid && heightValid && eyeColorsValid && passportValid && hairValid && expiryYearValid;
    } }
}