-- Kick
local spellFX = 0x0A286E2E  --Spell Effect
local recast = 300 --Recast Time
local castTime = 0
local damage = -115
function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
    Damage(session, damage, target)
end

function completeSpell()
end

function useItem()
    print("Using Milk")
    LearnSpell(session, GetSpell(170))
end
