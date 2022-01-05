using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public bool IsFacingRight = true;

    public DataMovement Data;

    public int GetFacingDirection()
    {
        return this.IsFacingRight ? 1 : -1;
    }

    public Vector2 DetermineDashDestination(Transform entityTransform)
    {
        Vector2 dashPosition = new Vector2(entityTransform.transform.position.x, entityTransform.transform.position.y)
            + (new Vector2(Data.DashForce, 0.0005f) * GetFacingDirection());

        RaycastHit2D raycastHit2D = Physics2D.Raycast(entityTransform.transform.position,
            Vector2.right * GetFacingDirection(),
            Mathf.Abs(((Vector2)entityTransform.transform.position - dashPosition).x),
            (Data.WhatIsEnemy | Data.WhatIsGround));

        if (raycastHit2D)
        {
            if (raycastHit2D.collider.tag.Equals("Enemy"))
            {
                return dashPosition;
            }
            else
            {
                var distance = raycastHit2D.distance;
                distance -= Data.SafetyOffsetX;
                distance = distance > 0 ? distance : 0;
                return (Vector2)entityTransform.transform.position + (Vector2.right * GetFacingDirection() * distance);
            }
        }
        return dashPosition - (Vector2.right * GetFacingDirection() * Data.SafetyOffsetX);
    }

    public Vector2 DetermineEvadePosition(Transform entityTransform)
    {
        Vector2 dashPosition = new Vector2(entityTransform.position.x, entityTransform.position.y)
            - (GetFacingDirection() * new Vector2(Data.DashForce / 5, 0));

        RaycastHit2D raycastHit2D = Physics2D.Raycast(entityTransform.position,
            Vector2.right * GetFacingDirection(),
            IsFacingRight ? dashPosition.x - entityTransform.position.x : entityTransform.position.x - dashPosition.x,
            (Data.WhatIsEnemy | Data.WhatIsGround));

        if (raycastHit2D.collider != null)
        {
            dashPosition = raycastHit2D.point;
        }
        return dashPosition - (Vector2.right * GetFacingDirection() * Data.SafetyOffsetX);
    }

    public Vector2 DetermineSlashPosition(Transform entityTransform)
    {
        //Collider2D hitEnemies = Physics2D.OverlapCircle(AttackPosition.position, Data.AttackRange, Data.WhatIsEnemy);
        Vector3 slashPosition = new Vector2(entityTransform.position.x, entityTransform.position.y)
        + (new Vector2(Data.SlashForce, 0) * GetFacingDirection());

        RaycastHit2D raycastHit2D = Physics2D.Raycast(entityTransform.position,
            Vector2.right * GetFacingDirection(),
            IsFacingRight ? slashPosition.x - entityTransform.position.x : entityTransform.position.x - slashPosition.x,
            (Data.WhatIsEnemy | Data.WhatIsGround));

        if (raycastHit2D.collider != null)
        {
            slashPosition = raycastHit2D.point;
        }

        return slashPosition;
    }

    public Vector2 DetermineStabPosition(Transform entityTransform)
    {
        Vector2 stabPosition = new Vector2(entityTransform.position.x, entityTransform.position.y)
            + (new Vector2(Data.StabForce, 0) * GetFacingDirection());

        RaycastHit2D raycastHit2D = Physics2D.Raycast(entityTransform.position,
            Vector2.right * GetFacingDirection(),
            IsFacingRight ? stabPosition.x - entityTransform.position.x : entityTransform.position.x - stabPosition.x,
            (Data.WhatIsEnemy | Data.WhatIsGround));

        if (raycastHit2D)
        {
            stabPosition = raycastHit2D.point;
        }
        return stabPosition - (Vector2.right * GetFacingDirection() * Data.SafetyOffsetX);
    }

    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        IsFacingRight = !IsFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = Core.transform.parent.transform.localScale;
        theScale.x *= -1;
        Core.transform.parent.transform.localScale = theScale;
    }
}
