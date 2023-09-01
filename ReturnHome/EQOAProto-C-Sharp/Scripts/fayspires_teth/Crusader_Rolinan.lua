function event_say()
diagOptions = {}
    npcDialogue = "Send me to the front of our next offensive with the dark elves. I will slay them all for their treachery and destruction of our true home, Takish'Hiz!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end