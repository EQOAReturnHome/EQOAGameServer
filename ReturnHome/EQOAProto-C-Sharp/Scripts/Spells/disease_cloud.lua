-- Disease Cloud
local spellFX = 0x052EF90F  --Spell Effect
local recast = 1 --Recast Time
local castTime = 1

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
end

function useItem()
end

