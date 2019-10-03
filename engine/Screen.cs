using System;

namespace MVC.Engine {

  public enum ColorType {
    Default = ConsoleColor.Green,
    Warning = ConsoleColor.Red,
    Core = ConsoleColor.Blue,
    Engine = ConsoleColor.Yellow,
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
      Log(type + ": " + text + "\n", (ConsoleColor)type);
    }

    public static void Log(string text) {
      Log(text, ColorType.Default);
    }
  }
}