-- Chilling Aura
local spellFX = 0x66CD778B  --Spell Effect
local recast = 10000 --Recast Time
local castTime = 20000
duration

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
--Proc cast buff goes here apply
end

function useItem()
end

