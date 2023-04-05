-- Return Home
local spellFX = 0x8FB28233  --Spell Effect
local recast = 300000 --Recast Time
local castTime = 30000

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
   print("Teleporting player")
   TeleportPlayer(session, boundWorld,boundX,boundY,boundZ,boundFacing)
end

function useItem()
    print("Using Return Home Item")
    LearnSpell(session, GetSpell(1))
end

