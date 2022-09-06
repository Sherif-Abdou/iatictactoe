// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using IATicTacToe;

var TIMES = 1;

double average_time = 0.0;
int w = 6;
int h = 2;

var timer = Stopwatch.StartNew();
var board = new Board(w, h);
var bot2 = new Bot(Slot.Cross, board);
bot2.MakeMove();
timer.Stop();
var millis = timer.ElapsedMilliseconds;
var possible = bot2.cache.Count;
Console.WriteLine($"Ratio: {(double)(possible*(1))/millis}");


// var board2 = new Board(2, 2);
// var bot3 = new Bot(Slot.Cross, board2)
// {
//     CanWin = false
// };
// bot3.MakeMove();
// var difference = bot3.cache.Keys.Except(bot2.cache.Keys).ToList();
// var timer = System.Diagnostics.Stopwatch.StartNew();

Console.WriteLine($"Average game time among {TIMES} runs: {average_time}");