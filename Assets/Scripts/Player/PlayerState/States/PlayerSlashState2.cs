using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlashState2 : PlayerSlashState
{
    public PlayerSlashState2(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    protected override void SwitchSlashState()
    {
        stateMachine.ChangeState(player.SlashState);
    }

}
