using System;
using System.IO;
using System.Linq;

static void TwoNumbers(int[] numbers) {
    for(int i = 0; i < numbers.Length; i++){
        for(int y = 0; y < numbers.Length; y++){
            int number1 = numbers[i];
            int number2 = numbers[y];
            if(number1 + number2 == 2020){
                int answer = number1 * number2;
                Console.WriteLine(answer);
                return;
            }
        }
    }
}

static void ThreeNumbers(int[] numbers) {
    for(int i = 0; i < numbers.Length; i++){
        for(int y = 0; y < numbers.Length; y++){
            for(int z = 0; z < numbers.Length; z++){
                int number1 = numbers[i];
                int number2 = numbers[y];
                int number3 = numbers[z];
                if(number1 + number2 + number3 == 2020){
                    int answer = number1 * number2 * number3;
                    Console.WriteLine(answer);
                    return;
                }
            }
        }
    }
}

string[] lines = await File.ReadAllLinesAsync("numbers.txt");
int[] numbers = lines.Select(x => int.Parse(x)).ToArray();

TwoNumbers(numbers);
ThreeNumbers(numbers);