--quick strike
local spellFX = 0x66CD778B  --Spell Effect
local recast = 30 --Recast Time
local castTime = 0
local damage = 10 + (session.MyCharacter.Strength*.40)


function startSpell()
    CastSpell(session, spellFX, entity.ObjectID, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
    Damage(session, damage, entity.ObjectID, target)
    entityTarget.CurrentHP = (entityTarget.CurrentHP-damage)
end

function useItem()
end

