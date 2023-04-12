-- Hide
local spellFX = 0x4280E1DA  --Spell Effect
local recast = 6 --Recast Time
local castTime = 0
local duration = 300

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
end

function useItem()
end

