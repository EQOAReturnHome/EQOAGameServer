function  event_say(choice)
diagOptions = {}
    npcDialogue = "If you don't mind yerself here in the Church of Bertoxxulous, the Church of Bertoxxulous will be paying close mind to you playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end