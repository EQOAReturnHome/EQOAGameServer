function event_say()
diagOptions = {}
    npcDialogue = "If you let me bind you here, you may return home to the safety of Tethelin at any time. Shall I do this playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end