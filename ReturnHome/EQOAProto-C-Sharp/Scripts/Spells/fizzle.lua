-- fizzle spell
local spellFX = 0x9EC7854A  --Spell Effect
local recast = 0 --Recast Time
local castTime = 0
function startSpell()
    CastSpell(session, spellFX, target, castTime)
end
