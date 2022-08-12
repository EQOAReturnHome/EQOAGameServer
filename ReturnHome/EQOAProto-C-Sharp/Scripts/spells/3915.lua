-- Boot
function spell()
    spellFX = 0x0A286E2E --Spell Effect Hex
    damage = 70
    recast = 6000  --Recast Time
CastSpell(session, spellFX, target)
    Damage(session, damage, target)
    CoolDown(session, addedOrder, recast)
end
