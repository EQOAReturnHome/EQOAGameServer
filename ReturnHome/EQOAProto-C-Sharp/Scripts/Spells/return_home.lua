-- Return Home
local spellFX = 0x8FB28233  --Spell Effect
local recast = 300 --Recast Time
local castTime = 30

function startSpell()
    print(session)
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
   TeleportPlayer(session, boundWorld,boundX,boundY,boundZ,boundFacing)
end

function useItem()
end
