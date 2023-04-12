-- Return Home
local spellFX = 0x725CFDB8  --Spell Effect
local recast = 4 --Recast Time
local castTime = 3

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
end

function useItem()
end

