-- Chilling Aura
local spellFX = 0xF3C09208  --Spell Effect
local recast = 1 --Recast Time
local castTime = 2
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

