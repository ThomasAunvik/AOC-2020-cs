using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

string[] lines = await File.ReadAllLinesAsync("passports.txt");

List<Passport> passports = new List<Passport>();
Dictionary<string, string> passportValues = new Dictionary<string, string>();

for(int passportIndex = 0; passportIndex < lines.Length; passportIndex++){
    string line = lines[passportIndex];
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
        return birthYear != 0 && issueYear != 0 && expirationYear != 0 && 
                !string.IsNullOrEmpty(height) &&
                !string.IsNullOrEmpty(hairColor) &&
                !string.IsNullOrEmpty(eyeColor) &&
                passportId != 0;
    } }
}