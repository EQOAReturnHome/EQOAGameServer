﻿-- Kick
local spellFX = 0x0A286E2E  --Spell Effect
local recast = 300 --Recast Time
local castTime = 0
local damage = -115
--Spell testSpell = new Spell(179, 1, 0, 0, 1, 1, 13, 1, 1, 1, 1, 0, 486870879, 638503769, 0, 0, 255,"Kick", "An attack that allows you to kick your enemy.");

local spellID

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
    Damage(session, damage, target)
end

function completeSpell()
   TeleportPlayer(session, boundWorld,boundX,boundY,boundZ,boundFacing)
end

function useItem()
    LearnSpell()
end
