function  event_say(choice)
diagOptions = {}
    npcDialogue = "STAY AWAY! STAY AWAY FROM ME, OR I'LL SET YOU ABLAZE!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end