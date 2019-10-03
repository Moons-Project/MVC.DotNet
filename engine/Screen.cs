using System;

namespace MVC.Engine {

  public enum ColorType {
    Green = ConsoleColor.Green,
    Red = ConsoleColor.Red,
    Yellow = ConsoleColor.Yellow
  }

  public static class Screen {
    public static void Log(string text, 
      ConsoleColor textColor = ConsoleColor.White,
      ConsoleColor bgColor = ConsoleColor.Black) {
        Console.BackgroundColor = bgColor;
        Console.ForegroundColor = textColor;
        Console.Write(text);
        Console.ResetColor();
      }
    
    public static void Log(string text, ColorType type) {
      Log(text, (ConsoleColor)type);
    }
  }
}