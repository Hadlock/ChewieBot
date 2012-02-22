require 'net/http'



def check_file_for_things filename, url
	begin
	File.open(filename,"r+") do |f|
		output = ""
		f.each do |line|
			puts "parsing line #{line}"
#if !anything		if line =~/\s!.*\w/
			if line =~/\s!lolocaust/
				puts "line #{line} found and removed calling #{url}"
				html = Net::HTTP.get(URI.parse(url))
				system("C:\\processing.exe")
				system("C:\\timeanddate.exe")				
				system("C:\\broken.exe")
				system("C:\\messageends.exe")

			else
				output << line
			end
		end
		f.pos = 0                     
		f.print output
		f.truncate(f.pos)   
	end

	rescue => e
		puts "something had gone wrong: #{e}"
	end
end

filename = 'c:\steamlogs\Steam Group Name.txt'
url = 'http://domain.com/command/command.php'

while (true)
	puts "checking file #{filename} for things"
	check_file_for_things filename, url
	puts "sleeping 15 seconds..."
	sleep(15)
	puts "i'm awake"
end