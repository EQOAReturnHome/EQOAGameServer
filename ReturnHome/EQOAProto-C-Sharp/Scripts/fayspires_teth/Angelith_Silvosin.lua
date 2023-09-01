function event_say()
diagOptions = {}
    npcDialogue = "In order for our scouts to survive, they need a proper dagger. There are many skills one can perform with such a small and discrete weapon."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end