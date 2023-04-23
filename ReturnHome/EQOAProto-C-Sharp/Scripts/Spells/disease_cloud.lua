-- Disease Cloud
local spellFX = 0x052EF90F  --Spell Effect
local recast = 1 --Recast Time
local castTime = 1
local buffName = "Disease Cloud"
local effectID = effects.MINOR_HEALING
local duration = 18
local heal = 16
local tier = 0

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
    if(session.MyCharacter.CurrentHP ~= session.MyCharacter.HPMax)then
    Heal(session, heal, target)
    session.MyCharacter.CurrentHP = (heal+session.MyCharacter.CurrentHP)
    end
    StatusEffect(effectID, buffName, buffIcon, duration, tier)
end

function tickSpell()
     if(session.MyCharacter.CurrentHP ~= session.MyCharacter.HPMax)then
     Heal(session, heal, target)
     session.MyCharacter.CurrentHP = (heal+session.MyCharacter.CurrentHP)
     end
end

function useItem()
end
