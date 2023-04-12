--Unstable Mixture
local items = require('Scripts/items')
local spellFX = 0xA8C2C199     --Spell Effect
local recast = 2 --Recast Time
local castTime = 1
local potionsCreated = 3

function startSpell()
    CastSpell(session, spellFX, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
    GrantItem(session, items.UNSTABLE_MUTATION, potionsCreated)
end

function useItem()
end

