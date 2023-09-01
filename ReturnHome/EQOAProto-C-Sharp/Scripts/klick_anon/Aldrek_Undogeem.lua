function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm afraid I won't be sharing with you my private business, especially down here in the bowels of Klick'Anon."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end