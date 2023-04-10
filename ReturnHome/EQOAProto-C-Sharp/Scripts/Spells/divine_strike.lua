-- Return Home
local spellFX = 0x66CD778B  --Spell Effect
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

