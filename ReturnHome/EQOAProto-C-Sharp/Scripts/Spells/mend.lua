-- Return Home
local spellFX = 0xC2B700FA   --Spell Effect
local recast = 90 --Recast Time
local castTime = 1
local health = 45 + (session.MyCharacter.Wisdom * .10)

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
    Heal(session, health, target)
end

function useItem()
end

