#include <sys/sysctl.h>

extern "C" {
	const double uptime() {
        struct timeval boottime;
        size_t len = sizeof(boottime);
        int mib[2] = { CTL_KERN, KERN_BOOTTIME };
        if( sysctl(mib, 2, &boottime, &len, NULL, 0) < 0 )
        {
            return -1.0;
        }
        time_t bsec = boottime.tv_sec, csec = time(NULL);
        
        return difftime(csec, bsec);
	}
}