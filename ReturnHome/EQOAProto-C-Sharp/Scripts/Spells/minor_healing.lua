--Minor Healing
local spellFX = 0xC2B700FA  --Spell Effect
local recast = 1 --Recast Time
local castTime = 2

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
end

function useItem()
end

