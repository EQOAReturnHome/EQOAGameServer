function event_say()
diagOptions = {}
    npcDialogue = "If you wish to explore Tethelin, you would do well to mind your footing while high up in the trees."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end