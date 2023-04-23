-- Burst of Frost
local spellFX = 0x5C0E59CE  --Spell Effect
local recast = 3 --Recast Time
local castTime = 2
local damage = 46 + (session.MyCharacter.Dexterity*.05)

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
    Damage(session, damage, target)
    entityTarget.CurrentHP = (entityTarget.CurrentHP-damage)


end

function useItem()
end
