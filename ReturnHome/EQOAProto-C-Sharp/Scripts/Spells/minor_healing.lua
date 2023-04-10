--Minor Healing
local spellFX = 0xC2B700FA  --Spell Effect
local recast = 300000 --Recast Time
local castTime = 30000

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
end

function useItem()
end

