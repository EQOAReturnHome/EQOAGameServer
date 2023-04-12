-- Kick
local spellFX = 0x0A286E2E  --Spell Effect
local recast = 6 --Recast Time
local castTime = 0
local damage = 16 + (mySession.MyCharacter.Strength*.40)

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
    Damage(session, damage, target)
end

function useItem()
end
