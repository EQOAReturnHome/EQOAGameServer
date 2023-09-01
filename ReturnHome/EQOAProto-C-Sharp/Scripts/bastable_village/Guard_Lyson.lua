function event_say()
diagOptions = {}
    npcDialogue = "Welcome to Bastable. We are a quiet village and us guards are paid to keep it that way. While you're here, grab a drink and hot meal from Bella."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end