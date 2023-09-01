function event_say()
diagOptions = {}
    npcDialogue = "Welcome to the Celoon griffon stables! Make no sudden movements and always bow before you approach a griffon."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end