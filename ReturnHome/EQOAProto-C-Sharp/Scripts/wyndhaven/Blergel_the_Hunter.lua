function  event_say(choice)
diagOptions = {}
    npcDialogue = "Sorry playerName, I can't help you. I am quite busy hunting down ivory tusks for Madam Thlae."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end