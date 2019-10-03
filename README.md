# MVC.DotNet

[![](https://img.shields.io/badge/Author-jskyzero-brightgreen.svg?style=flat)]()
[![](https://img.shields.io/badge/Data-2019/10/03-brightgreen.svg?style=flat)]()
[![](https://img.shields.io/badge/netcoreapp-3.0-brightgreen.svg?style=flat)]()

## Overview

a mvc structure example implement in dotnet (C#)

```CSharp
  class GameStart : CallerBehaviour {
    public void Run() {
      Call(new PrintCommand());
      Call(new CompareAttackCommand(10));

      Call("MVC.Main.PrintCommand");
    }
  }
```
