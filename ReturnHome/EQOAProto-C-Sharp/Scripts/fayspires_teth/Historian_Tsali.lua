function event_say()
diagOptions = {}
    npcDialogue = "A collection of totems I have coveted, have been scattered and lost. If we can find them, something great and terrible awaits us in the end."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end