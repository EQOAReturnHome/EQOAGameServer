function event_say()
diagOptions = {}
    npcDialogue = "A group of our elves were traveling from Tethelin with some of the Elddar Tomes. They have been gone too long and I am worried for their safety. If they aren't back soon, we will have to send out a search party."
SendDialogue(mySession, npcDialogue, diagOptions)
end