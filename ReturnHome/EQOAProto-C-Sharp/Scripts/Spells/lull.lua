-- Lull
local spellFX = 0xAE2F114E  --Spell Effect
local recast = 2 --Recast Time
local castTime = 1

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
end

function useItem()
end

