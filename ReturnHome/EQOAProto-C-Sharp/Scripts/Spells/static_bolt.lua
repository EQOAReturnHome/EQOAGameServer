-- Return Home
local spellFX = 0xB12F1DAB   --Spell Effect
local recast = 1 --Recast Time
local castTime = 1
local damage = 70 * (session.MyCharacter.Dexterity*.05)

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
    Damage(session, damage, target)
end

function useItem()
end

