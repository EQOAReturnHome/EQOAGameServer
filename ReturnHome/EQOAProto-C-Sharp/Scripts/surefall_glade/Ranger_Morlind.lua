function  event_say(choice)
diagOptions = {}
    npcDialogue = "Long have the Protectors of the Pine stood guard here at Surefall Glade. Spend some time here and you will know why."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end