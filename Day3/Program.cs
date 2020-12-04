using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

string[] lines = await File.ReadAllLinesAsync("terrain.txt");

TreesTask2(lines);

static void TreesTask2(string[] lines){
    int linePosition = 0;
    int maxLinePosition = lines[0].Length-1;

    int treeCount = 0;
    List<int> treesFound = new List<int>();

    List<(int, int, int)> instructions = new List<(int, int, int)>(){
        (1, 1, 0),
        (3, 1, 0),
        (5, 1, 0),
        (7, 1, 0),
        (1, 2, 0)
    };
    int executionCount = 0;

    for(int treeIndex = 0; treeIndex < lines.Length;) {
        var line = lines[treeIndex];
        var execution = instructions[executionCount];

        int treesInLine = 0;
        int originalLinePos = linePosition;

        var print = line.Remove(linePosition, 1).Insert(linePosition, "O");

        for(int linePos = originalLinePos; linePos < originalLinePos + execution.Item1; linePos++){
            linePosition++;

            if(linePosition > maxLinePosition) 
                linePosition -= maxLinePosition+1;

            print = print.Remove(linePosition, 1).Insert(linePosition, "O");

            if(line[linePosition] == '#') {
                treeCount++;
                treesInLine++;
                print = print.Remove(linePosition, 1).Insert(linePosition, "X");
            }
        }

        Console.WriteLine(print);

        for(int down = treeIndex+1; down < treeIndex + execution.Item2; down++){
            if(down >= lines.Length) continue;

            var newprint = lines[down].Remove(linePosition, 1).Insert(linePosition, "O");

            char current = lines[down][linePosition];
            if(line[linePosition] == '#') {
                treeCount++;
                treesInLine++;
                newprint = newprint.Remove(linePosition, 1).Insert(linePosition, "X");
            }

            Console.WriteLine(newprint);
        }
        treeIndex += execution.Item2;

        execution.Item3 += treesInLine;
        instructions[executionCount] = execution;
        
        executionCount++;
        if(executionCount >= instructions.Count) executionCount = 0;
    }

    long answer = 1;
    for(int i = 0; i < instructions.Count; i++)  {
        answer *= instructions[i].Item3;
    }

    Console.WriteLine("Trees Encountered: {0}", treeCount);
    Console.WriteLine("Answer to Task2: {0}", answer);
}

static void TreesTask1(string[] lines){
    int linePosition = 0;
    int maxLinePosition = lines[0].Length-1;

    int trees = lines.Count(x => {
        bool isTree = x[linePosition] == '#';
        var print = x.Remove(linePosition, 1).Insert(linePosition, "O");

        Console.WriteLine(print);

        linePosition += 3;
        if(linePosition > maxLinePosition) 
            linePosition -= maxLinePosition+1;

        return isTree;
    });

    Console.WriteLine("Trees Encountered: {0}", trees);
}