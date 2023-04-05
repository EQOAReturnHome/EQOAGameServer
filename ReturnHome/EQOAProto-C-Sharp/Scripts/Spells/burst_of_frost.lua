-- Burst of Frost
local spellFX = 0x5C0E59CE  --Spell Effect
local recast = 3000 --Recast Time
local castTime = 2000
local damage = -1 * (session.MyCharacter.Dexterity*.05)
function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
    Damage(session, damage, target)
end

function useItem()
    print("Using Burst of Frost")
    LearnSpell(session, GetSpell(2))
end