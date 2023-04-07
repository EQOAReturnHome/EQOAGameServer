-- Kick
local spellFX = 0x0A286E2E  --Spell Effect
local recast = 300 --Recast Time
local castTime = 0
local damage = -115

print("Kick")

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
    Damage(session, damage, target)
end

function useItem()
end
