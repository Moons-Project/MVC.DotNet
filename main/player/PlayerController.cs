using MVC.Engine;
using MVC.Core;

namespace MVC.Main {

using static PlayerCommand;

  public sealed class PlayerController : 
    Controller<PlayerModel, PlayerView> {

      public void OnPrintCommand(PrintCommand cmd) {
        System.Console.WriteLine(@$"
          HP: {this.Model.health}
          Attack: {this.Model.attack}"
        );
      }

      public void OnCompareAttackCommand(CompareAttackCommand cmd) {
        System.Console.WriteLine(@$"
          Your Attack: {this.Model.attack}
          His Attack: {cmd.OtherAttack}
          Result: {this.Model.attack > cmd.OtherAttack}"
        );
      }

  }
}