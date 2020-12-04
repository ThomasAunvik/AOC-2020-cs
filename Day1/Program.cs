using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

static List<NumberAnswer> TwoNumbers(int[] numbers) {
    List<NumberAnswer> answers = new List<NumberAnswer>();

    for(int i = 0; i < numbers.Length; i++){
        for(int y = 0; y < numbers.Length; y++){
            int number1 = numbers[i];
            int number2 = numbers[y];
            if(number1 + number2 == 2020){
                int answer = number1 * number2;

                answers.Add(new NumberAnswer() {
                    answer = answer,
                    numbers = new int[] {
                        number1, number2
                    }
                });

                return answers;
            }
        }
    }
    return answers;
}

static List<NumberAnswer> TwoNumbersLinq(int[] numbers) {
    List<NumberAnswer> answers = new List<NumberAnswer>();

    var num = numbers.ToList();
    num.ForEach(x => {
        num.ForEach(y => {
            if(x + y == 2020){
                int answer = x * y;

                answers.Add(new NumberAnswer() {
                    answer = answer,
                    numbers = new int[] {
                        x, y
                    }
                });
            }
        });
    });

    return answers;
}

static List<NumberAnswer> ThreeNumbers(int[] numbers) {
    List<NumberAnswer> answers = new List<NumberAnswer>();

    for(int i = 0; i < numbers.Length; i++){
        for(int y = 0; y < numbers.Length; y++){
            for(int z = 0; z < numbers.Length; z++){
                int number1 = numbers[i];
                int number2 = numbers[y];
                int number3 = numbers[z];

                if(number1 + number2 + number3 == 2020){
                    int answer = number1 * number2 * number3;

                    answers.Add(new NumberAnswer() {
                        answer = answer,
                        numbers = new int[] {
                            number1, number2, number3
                        }   
                    });

                    return answers;
                }
            }
        }
    }
    return answers;
}

static List<NumberAnswer> ThreeNumberLinq(int[] numbers) {
    List<NumberAnswer> answers = new List<NumberAnswer>();

    var num = numbers.ToList();
    num.ForEach(x => {
        num.ForEach(y => {
            num.ForEach(z => {
                if(x + y + z == 2020){
                    int answer = x * y * z;

                    answers.Add(new NumberAnswer() {
                        answer = answer,
                        numbers = new int[] {
                            x, y, z
                        }
                    });
                }
            });
        });
    });

    return answers;
}

string[] lines = await File.ReadAllLinesAsync("numbers.txt");
int[] numbers = lines.Select(x => int.Parse(x)).ToArray();

var start = DateTime.Now;
var twos = TwoNumbers(numbers);
var msTwo = DateTime.Now - start;

start = DateTime.Now;
var threes = ThreeNumbers(numbers);
var msThree = DateTime.Now - start;

Console.WriteLine("Answers for Twos:\n{0}", string.Join("\n", twos.Select(x => "\nAnswer: " + x.answer + "\nNumbers: " + string.Join(", ", x.numbers))));
Console.WriteLine("(ms: {0})", msTwo.TotalMilliseconds);

Console.WriteLine("\n\nAnswers for Threes:\n{0}", string.Join("\n", threes.Select(x => "\nAnswer: " + x.answer + "\nNumbers: " + string.Join(", ", x.numbers))));;
Console.WriteLine("(ms: {0})", msThree.TotalMilliseconds);

struct NumberAnswer {
    public int[] numbers;
    public int answer;
}